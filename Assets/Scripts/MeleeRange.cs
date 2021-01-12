using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRange : MonoBehaviour
{

    public Dictionary<string, GameObject> enemiesInRange;
    public int enemies;
    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange = new Dictionary<string, GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        enemies = enemiesInRange.Count;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            try
            {
                enemiesInRange.Add(other.name, other.gameObject);

            }
            catch (System.ArgumentException)
            { }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            try
            {
                enemiesInRange.Remove(other.name);

            }
            catch (System.ArgumentException)
            { }
        }
    }
}
