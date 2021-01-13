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
    [SerializeField]
    GameObject swordType;

    GameObject heldSword;
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
        if (heldSword == null)
        {
            Vector3 spawnPoint = transform.position + transform.forward * 1 + transform.right * 1;
            heldSword = Instantiate(swordType, spawnPoint, transform.rotation);
            heldSword.transform.Rotate(0, 45, 0, Space.World);
            heldSword.transform.parent = transform;
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
