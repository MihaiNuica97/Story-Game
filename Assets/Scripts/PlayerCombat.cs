using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PlayerCombat : MonoBehaviour
{
    // only cast sphere against enemies
    int layerMask = (1 << 9);
    RaycastHit[] hits;

    [SerializeField]
    GameObject arrowType;


    public bool hitInitiated = false;

    public float hitCooldown = 0.5f;
    public MeleeRange rangeScript;
    Dictionary<string, GameObject> enemiesInRange;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !hitInitiated)
        {
            hitInitiated = true;
            StartCoroutine(InitiateCooldown());
            meleeAttack();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && !hitInitiated)
        {
            hitInitiated = true;
            StartCoroutine(InitiateCooldown());
            rangedAttack();
        }
    }
    void meleeAttack()
    {
        foreach (GameObject enemy in rangeScript.enemiesInRange.Values)
        {
            enemy.SendMessage("TakeDamage", 50);
        }
    }

    void rangedAttack()
    {
        Vector3 spawnPoint = transform.position + transform.forward * 1;
        Instantiate(arrowType, spawnPoint, transform.rotation);
    }

    IEnumerator InitiateCooldown()
    {
        yield return new WaitForSeconds(hitCooldown);
        hitInitiated = false;
        // Code to execute after the delay
    }
}
