using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectPrompt : MonoBehaviour
{
    Interaction playerScript;
    Transform prompt;
    public bool visible = false;
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
            visible = true;
            prompt.gameObject.SetActive(true);
        }
        else
        {
            visible = false;
            prompt.gameObject.SetActive(false);
        }
    }
}
