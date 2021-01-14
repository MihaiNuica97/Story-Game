﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    public Dictionary<string, bool> questStatus;
    public Dictionary<string, bool> upgrades;
    Quest currentQuest;

    public string currentQuestTitle;
    public string currentQuestDescription;
    public int currentQuestProgress;

    Text titleText;
    Text descriptionText;
    Text progressText;
    ScriptParser completionParser;
    string completionScript;
    // Start is called before the first frame update
    void Start()
    {
        questStatus = new Dictionary<string, bool>();
        upgrades = new Dictionary<string, bool>();
        upgrades.Add("Dash", false);
        upgrades.Add("Armor", false);
        upgrades.Add("Greatsword", false);
        upgrades.Add("Longbow", false);
        titleText = GameObject.Find("Title").GetComponent<Text>();
        descriptionText = GameObject.Find("Description").GetComponent<Text>();
        progressText = GameObject.Find("Progress").GetComponent<Text>();
    }

    void Update()
    {
        if (currentQuest != null)
        {
            currentQuestTitle = currentQuest.title;
            currentQuestDescription = currentQuest.description;
            currentQuestProgress = currentQuest.progress;
            progressText.text = currentQuest.progress.ToString() + "/" + currentQuest.goal.ToString();
            if (currentQuest.goal == currentQuest.progress)
            {
                Debug.Log("Completed quest: " + currentQuest.title);
                completionParser.changeScriptFile(completionScript);
                bool currentQuestStatus;
                questStatus.TryGetValue(currentQuest.title, out currentQuestStatus);
                if (!currentQuestStatus)
                {
                    questStatus[currentQuest.title] = true;
                }

            }
        }
        else
        {
            titleText.text = "";
            descriptionText.text = "";
            progressText.text = "";
        }
    }

    public void CompleteQuest()
    {
        currentQuest.progress = currentQuest.goal;
    }
    public void StartQuest(string title, string description, int goal, ScriptParser completionParser, string completionScript)
    {
        currentQuest = new Quest(title, description, goal);
        if (completionScript != null)
        {
            this.completionParser = completionParser;
            this.completionScript = completionScript;
        }
        titleText.text = title;
        descriptionText.text = description;
        questStatus.Add(title, false);
    }

    public void ProgressCurrentQuest()
    {
        if (currentQuest != null)
        {

            currentQuest.Progress();
        }
    }

    class Quest
    {
        public string title;
        public string description;

        public int goal;
        public int progress;
        public Quest(string title, string description, int goal)
        {
            this.title = title;
            this.description = description;
            this.goal = goal;
            this.progress = 0;
        }
        public void Progress()
        {
            if (progress < goal)
            {
                progress++;
            }

        }
    }

    // Upgrades
    public void EnableUpgrade(string upgrade)
    {
        upgrades[upgrade] = true;
    }
    public void DisableUpgrade(string upgrade)
    {
        upgrades[upgrade] = false;
    }
}
