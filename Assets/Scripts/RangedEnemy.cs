using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    Transform player;
    public int health;

    CharacterController characterController;
    [SerializeField]
    GameObject arrowType;
    public bool aggro = false;
    public float aggroRange = 20f;

    public float attackRange = 10f;
    public float speed = 5f;
    public float hitCooldown = 0.5f;
    public bool hitInitiated = false;

    int mask = ~(1 << 9);
    float playerDistance;
    RaycastHit hit;
    float initialElevation;
    QuestLog questLog;


    // public float playerDistance;
    // Start is called before the first frame update
    void Start()
    {
        initialElevation = transform.position.y;
        questLog = GameObject.Find("Quest Tracker").GetComponent<QuestLog>();

        // health = 100;
        player = GameObject.Find("Player").transform;
        characterController = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    public virtual void Update()
    {

        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        // if player is within engage distance, check if he is within aggro distance and in view
        if (hit.transform != null)
        {
            aggro = (hit.transform.name == player.name && playerDistance <= aggroRange);
        }

        // aggro behaviour
        if (aggro)
        {
            transform.LookAt(player);
            if (playerDistance >= attackRange)
            {
                characterController.Move(transform.forward * Time.deltaTime * speed);
            }
            else
            {
                if (!hitInitiated)
                {
                    hitInitiated = true;
                    StartCoroutine(InitiateCooldown());
                    attack();
                }
            }
        }

        if (health == 0)
        {
            questLog.ProgressCurrentQuest();

            gameObject.SetActive(false);
        }
        if (transform.position.y != initialElevation)
        {
            transform.position = new Vector3(transform.position.x, initialElevation, transform.position.z);
        }

    }

    void attack()
    {
        Vector3 spawnPoint = transform.position + transform.forward * 2;
        Instantiate(arrowType, spawnPoint, transform.rotation);
    }
    IEnumerator InitiateCooldown()
    {
        yield return new WaitForSeconds(hitCooldown);
        hitInitiated = false;
        // Code to execute after the delay
    }

    private void FixedUpdate()
    {
        Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit, aggroRange, mask);
    }
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            health = 0;
        }
    }



}
