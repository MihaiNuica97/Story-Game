using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 50;
    bool alreadyTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!alreadyTriggered)
        {
            other.gameObject.SendMessage("TakeDamage", damage);
            alreadyTriggered = true;
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
    }
}
