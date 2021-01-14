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
    GameObject choicesPanel;

    GameObject player;
    void Start()
    {
        choicesPanel = GameObject.Find("Choices");
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
                if (args.Count == 4)
                {
                    questLog.StartQuest(sanitizeInput(args[0]), sanitizeInput(args[1]), Int32.Parse(sanitizeInput(args[2])), parser, sanitizeInput(args[3]));
                }
                else
                {
                    questLog.StartQuest(sanitizeInput(args[0]), sanitizeInput(args[1]), Int32.Parse(sanitizeInput(args[2])), parser, null);

                }
                break;

            case "changescript":
                if (args.Count == 1)
                {
                    parser.changeScriptFile(sanitizeInput(args[0]));
                }
                else
                {
                    ScriptParser other = GameObject.Find(sanitizeInput(args[0])).GetComponent<ScriptParser>();
                    other.changeScriptFile(sanitizeInput(args[1]));
                }
                break;
            case "releaseplayer":
                player.SendMessage("ReleaseMovement");
                break;
            case "lockplayer":
                player.SendMessage("LockMovement");
                break;
            case "completequest":
                questLog.CompleteQuest();
                break;
            case "enableupgrade":
                if (sanitizeInput(args[0]) == "Player")
                {
                    questLog.upgrades[sanitizeInput(args[1])] = true;
                }
                else if (sanitizeInput(args[0]) == "Boss")
                {
                    questLog.bossUpgrades[sanitizeInput(args[1])] = true;
                }
                break;
            case "choice":
                string choiceName = sanitizeInput(args[0]);
                choicesPanel.transform.Find(choiceName).gameObject.SetActive(true);
                break;
            case "bossaggro":
                Instantiate(Resources.Load<GameObject>("Boss"), transform.position, transform.rotation);
                this.gameObject.SetActive(false);
                break;
        }


    }
    string sanitizeInput(object input)
    {
        return input.ToString().Trim();
    }
}

