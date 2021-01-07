using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    Transform dialogueBox;

    private ScriptParser parser;
    [HideInInspector]
    public bool dialogueInitialized = false;

    InteractableObjectPrompt prompt;
    // Start is called before the first frame update
    void Start()
    {
        dialogueBox = transform.Find("Dialogue-Text");
        prompt = gameObject.GetComponent<InteractableObjectPrompt>();
        parser = gameObject.GetComponent<ScriptParser>();
    }

    // Update is called once per frame
    void Update()
    {
        dialogueBox.gameObject.SetActive(prompt.visible);
        if (!prompt.visible)
        {
            dialogueInitialized = false;
            parser.wipeDialogue();
        }
    }

    public void Interact()
    {
        if (!dialogueInitialized)
        {
            Debug.Log("Started Talking with: " + gameObject.name);
            parser.initializeDialogue();
            dialogueInitialized = true;
        }
        else
        {
            parser.progressDialogue();
        }

    }


}
