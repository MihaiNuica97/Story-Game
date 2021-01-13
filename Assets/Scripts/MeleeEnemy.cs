using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    Transform player;
    public int health;

    CharacterController characterController;

    [SerializeField] GameObject swordType;

    public bool aggro = false;
    public float aggroRange = 10f;

    int mask = ~(1 << 9);
    float playerDistance;
    public float speed = 5f;
    public float attackRange = 2f;

    public float hitCooldown = 0.5f;

    public bool hitInitiated = false;
    float initialElevation;

    RaycastHit hit;
    // public float playerDistance;
    // Start is called before the first frame update
    void Start()
    {
        initialElevation = transform.position.y;

        health = 100;
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


        if (aggro && !hitInitiated)
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
            gameObject.SetActive(false);
        }
        if (transform.position.y != initialElevation)
        {
            transform.position = new Vector3(transform.position.x, initialElevation, transform.position.z);
        }

    }
    IEnumerator InitiateCooldown()
    {
        yield return new WaitForSeconds(hitCooldown);
        hitInitiated = false;
        // Code to execute after the delay
    }
    void attack()
    {
        Vector3 spawnPoint = transform.position + transform.forward * 1 + transform.right * 2f;
        GameObject heldSword = Instantiate(swordType, spawnPoint, transform.rotation);
        heldSword.transform.Rotate(0, 60, 0, Space.World);
        heldSword.transform.parent = transform;
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
