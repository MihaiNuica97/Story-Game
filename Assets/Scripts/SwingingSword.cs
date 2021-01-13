using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingSword : MonoBehaviour
{
    public float movementSpeed = 9f;
    public float rotationSpeed = 250f;

    public float arcDegrees = 90f;
    Vector3 rotation = new Vector3(0, -1, 0);

    public int damage = 50;

    bool alreadyTriggered = false;

    Vector3 initialOrientation;
    Vector3 initialPosition;
    Quaternion initialRotation;

    /* Variables */

    void Start()
    {
        initialOrientation = transform.forward;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        // if (!alreadyTriggered)
        // {
        other.gameObject.SendMessage("TakeDamage", damage);
        // alreadyTriggered = true;
        // }
    }

    void Update()
    {
        float angle = Vector3.Angle(transform.forward, initialOrientation);
        if (angle < arcDegrees)
        {
            transform.Rotate(rotation * (rotationSpeed * Time.deltaTime), Space.World);
            transform.Translate(-transform.right * Time.deltaTime * movementSpeed, Space.World);
        }
        else
        {
            // transform.position = initialPosition;
            // transform.rotation = initialRotation;
            Destroy(this.gameObject);

        }

    }
}
