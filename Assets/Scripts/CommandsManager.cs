using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Globalization;

public class CommandsManager : MonoBehaviour
{
    DialogueSystem dialogue;
    QuestLog questLog;

    ScriptParser parser;

    GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        parser = gameObject.GetComponent<ScriptParser>();
        questLog = GameObject.Find("Quest Tracker").GetComponent<QuestLog>();
        // initialize();
    }
    // public override void initialize()
    // {
    //     // Debug.Log("Commands manager initialized");
    // }
    public void handle(string command)
    {
        // Debug.Log("Non-argument command found!");
    }

    public void handleWithArgs(string command, ArrayList args)
    {
        switch (command)
        {
            case "additive":
                if (args[0].ToString().ToLower().Trim().Equals("on"))
                {
                    dialogue.Say("");
                    dialogue.additiveTextEnabled = true;
                    Debug.Log("Set additive text to on!");
                }
                else if (args[0].ToString().ToLower().Trim().Equals("off"))
                {
                    dialogue.additiveTextEnabled = false;
                    Debug.Log("Set additive text to off!");
                }
                break;

            case "startquest":
                questLog.StartQuest(sanitizeInput(args[0]), sanitizeInput(args[1]), Int32.Parse(sanitizeInput(args[2])), parser, sanitizeInput(args[3]));
                break;

            case "changescript":
                parser.changeScriptFile(sanitizeInput(args[0]));
                break;
            case "releaseplayer":
                player.SendMessage("ReleaseMovement");
                break;
            case "lockplayer":
                player.SendMessage("LockMovement");
                break;

        }


    }
    string sanitizeInput(object input)
    {
        return input.ToString().Trim();
    }
}

