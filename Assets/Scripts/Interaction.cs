using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    CharacterController charCtrl;
    int layerMask = (1 << 8);
    public Transform interactingObject = null;

    RaycastHit[] hits;


    // Start is called before the first frame update
    void Start()
    {
        charCtrl = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.DrawRay(transform.position, transform.forward * 2);

        interactingObject = null;
        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        if (hits.Length != 0)
        {
            interactingObject = findNearest(hits);
            Debug.Log(interactingObject.name);
        }
        if (interactingObject != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interacted with: " + interactingObject.name);
            interactingObject.SendMessage("Interact");
        }

    }
    private void FixedUpdate()
    {
        Vector3 p1 = transform.position;
        hits = Physics.SphereCastAll(p1, 2, transform.forward, 1, layerMask);
    }
    private Transform findNearest(RaycastHit[] hits)
    {
        RaycastHit finalHit = hits[0];
        foreach (RaycastHit hit in hits)
        {
            if (hit.distance < finalHit.distance)
            {
                finalHit = hit;
            }
        }
        return finalHit.transform;
    }
}
