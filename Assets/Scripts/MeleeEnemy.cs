using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    Transform player;
    public int health;

    CharacterController characterController;

    public bool aggro = false;
    public float aggroRange = 10f;

    int mask = ~(1 << 9);
    float playerDistance;
    RaycastHit hit;
    // public float playerDistance;
    // Start is called before the first frame update
    void Start()
    {
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


        if (aggro)
        {
            transform.LookAt(player);
        }
        if (health == 0)
        {
            gameObject.SetActive(false);
        }

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
