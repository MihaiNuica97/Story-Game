using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectPrompt : MonoBehaviour
{
    Interaction playerScript;
    Transform prompt;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Interaction>();
        prompt = transform.Find("Interaction-Text");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.interactingObject == transform)
        {
            prompt.gameObject.SetActive(true);
        }
        else
        {
            prompt.gameObject.SetActive(false);
        }
    }
}
