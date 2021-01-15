using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    public float speed = 12f;
    public GameObject movement;
    float initialElevation;
    public bool movementLocked = false;
    public float dashCooldown = 2f;
    bool dashInitiated = false;
    Dictionary<string, bool> upgrades;
    // Start is called before the first frame update
    void Start()
    {
        initialElevation = transform.position.y;
        controller = gameObject.GetComponent<CharacterController>();
    }
    private void Awake()
    {
        // upgrades = GameObject.Find("Quest Tracker").GetComponent<QuestLog>().upgrades;

    }
    public void LockMovement()
    {
        movementLocked = true;
    }
    public void ReleaseMovement()
    {
        movementLocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (upgrades == null)
        {
            upgrades = GameObject.Find("Quest Tracker").GetComponent<QuestLog>().upgrades;
        }
        if (!movementLocked)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = movement.transform.forward * z + movement.transform.right * x;
            controller.Move(move * speed * Time.deltaTime);

            if (transform.position.y != initialElevation)
            {
                transform.position = new Vector3(transform.position.x, 0.6f, transform.position.z);
            }
            bool dashEnabled = false;
            upgrades.TryGetValue("Dash", out dashEnabled);
            if (Input.GetKeyDown(KeyCode.LeftShift) && dashEnabled && !dashInitiated)
            {
                dashInitiated = true;
                controller.Move(move * speed * Time.deltaTime * 20f);
                StartCoroutine(InitiateCooldown());
            }

        }
    }

    IEnumerator InitiateCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        dashInitiated = false;
        // Code to execute after the delay
    }
}
