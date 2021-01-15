using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PlayerCombat : MonoBehaviour
{
    public int health = 100;
    public int maxHP = 100;
    // only cast sphere against enemies
    int layerMask = (1 << 9);
    RaycastHit[] hits;

    [SerializeField]
    GameObject arrowType;
    GameObject normalArrow;
    [SerializeField]
    GameObject swordType;
    GameObject normalSword;
    [SerializeField]
    GameObject upgradedSword;
    [SerializeField]
    GameObject upgradedArrow;
    GameObject heldSword;
    public bool hitInitiated = false;

    public float hitCooldown = 0.5f;
    public MeleeRange rangeScript;
    Dictionary<string, GameObject> enemiesInRange;
    public Dictionary<string, bool> upgrades;
    private bool healthAlreadyUpgraded = false;

    private void Start()
    {
        upgrades = GameObject.Find("Quest Tracker").GetComponent<QuestLog>().upgrades;
        normalArrow = arrowType;
        normalSword = swordType;
        health = maxHP;
    }


    // Update is called once per frame
    void Update()
    {
        if (upgrades == null)
        {
            upgrades = GameObject.Find("Quest Tracker").GetComponent<QuestLog>().upgrades;
        }
        if (upgrades["Greatsword"] && swordType.name != "Greatsword")
        {
            swordType = upgradedSword;
        }
        else
        {
            swordType = normalSword;
        }
        if (upgrades["Longbow"] && arrowType.name != "Longbow Arrow")
        {
            arrowType = upgradedArrow;
        }
        else
        {
            arrowType = normalArrow;
        }
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
        if (!healthAlreadyUpgraded && upgrades["Armor"])
        {
            health *= 2;
            maxHP *= 2;
            healthAlreadyUpgraded = true;
        }
        if (healthAlreadyUpgraded && !upgrades["Armor"])
        {
            health /= 2;
            maxHP /= 2;
        }
        if (health == 0)
        {
            this.gameObject.SetActive(false);
        }
    }
    void meleeAttack()
    {
        if (heldSword == null)
        {
            Vector3 spawnPoint = transform.position + transform.forward * 2 + transform.right * 1;
            if (swordType.name == "Greatsword")
            {
                spawnPoint = transform.position + transform.forward * 1.5f + transform.right * 1;
            }

            heldSword = Instantiate(swordType, spawnPoint, transform.rotation);

            heldSword.transform.Rotate(0, 45, 0, Space.World);
            heldSword.transform.parent = transform;
        }

    }

    void rangedAttack()
    {
        Vector3 spawnPoint = transform.position + transform.forward * 1;
        GameObject arrow = Instantiate(arrowType, spawnPoint, transform.rotation);
        arrow.GetComponent<Projectile>().damagePlayer = false;
    }

    void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            health = 0;
        }
    }
    IEnumerator InitiateCooldown()
    {
        yield return new WaitForSeconds(hitCooldown);
        hitInitiated = false;
        // Code to execute after the delay
    }

    public void regenHP()
    {
        health = maxHP;
    }
}
