using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombat : MonoBehaviour
{

    Transform player;
    public int health;
    QuestLog log;
    CharacterController characterController;
    [SerializeField]
    GameObject arrowType;
    [SerializeField]

    GameObject swordType;
    [SerializeField]
    GameObject upgradedSword;


    [SerializeField]
    GameObject upgradedArrow;

    public bool aggro = true;
    public float rangedRange = 20f;

    public float meleeRange = 10f;

    public float speed = 5f;
    public float hitCooldown = 0.5f;
    public bool hitInitiated = false;

    int mask = ~(1 << 9);
    float playerDistance;
    RaycastHit hit;
    float initialElevation;
    QuestLog questLog;
    private bool healthUpgraded = false;

    bool dashed = false;

    float dashCooldown = 3f;
    // public float playerDistance;
    // Start is called before the first frame update
    void Start()
    {
        log = GameObject.Find("Quest Tracker").GetComponent<QuestLog>();
        aggro = true;
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

        if (log.bossUpgrades["Greatsword"] && swordType.name != "Boss Greatsword")
        {
            swordType = upgradedSword;
        }
        if (log.bossUpgrades["Longbow"] && arrowType.name != "Longbow Arrow")
        {
            arrowType = upgradedArrow;
        }
        // aggro behaviour
        if (aggro)
        {
            transform.LookAt(player);
            if (playerDistance >= rangedRange)
            {
                characterController.Move(transform.forward * Time.deltaTime * speed);
            }
            if (playerDistance >= meleeRange && log.bossUpgrades["Greatsword"])
            {
                characterController.Move(transform.forward * Time.deltaTime * speed);
            }
            if (playerDistance <= meleeRange && log.bossUpgrades["Longbow"])
            {
                characterController.Move(-transform.forward * Time.deltaTime * speed);
            }

            if (!hitInitiated)
            {
                if (log.bossUpgrades["Dash"] && !dashed)
                {
                    dashed = true;
                    StartCoroutine(InitiateDashCooldown());
                    characterController.Move(randomDirection() * Time.deltaTime * speed * 100f);
                }
                if (playerDistance < meleeRange)
                {
                    hitInitiated = true;
                    StartCoroutine(InitiateCooldown());
                    meleeAttack();
                }
                else
                {
                    hitInitiated = true;
                    StartCoroutine(InitiateCooldown());
                    rangedAttack();
                }
            }

        }
        if (!healthUpgraded && log.bossUpgrades["Armor"])
        {
            health *= 2;
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
    Vector3 randomDirection()
    {
        List<Vector3> vectors = new List<Vector3>();
        vectors.Add(transform.forward);
        vectors.Add(-transform.forward);
        vectors.Add(transform.right);
        vectors.Add(-transform.right);
        return vectors[Random.Range(0, vectors.Count)];
    }
    void rangedAttack()
    {
        Vector3 spawnPoint = transform.position + transform.forward * 1;
        Instantiate(arrowType, spawnPoint, transform.rotation);
    }
    void meleeAttack()
    {
        Vector3 spawnPoint = transform.position + transform.forward * 1 + transform.right * 2f;
        GameObject heldSword = Instantiate(swordType, spawnPoint, transform.rotation);
        heldSword.transform.Rotate(0, 60, 0, Space.World);
        heldSword.transform.parent = transform;
    }
    IEnumerator InitiateCooldown()
    {
        yield return new WaitForSeconds(hitCooldown);
        hitInitiated = false;
        // Code to execute after the delay
    }
    IEnumerator InitiateDashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        dashed = false;
        // Code to execute after the delay
    }
    private void FixedUpdate()
    {
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
