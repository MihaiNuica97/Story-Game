using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
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
            }
        }
        else
        {
            titleText.text = "";
            descriptionText.text = "";
            progressText.text = "";
        }
    }

    public void StartQuest(string title, string description, int goal, ScriptParser completionParser, string completionScript)
    {
        currentQuest = new Quest(title, description, goal);
        this.completionParser = completionParser;
        this.completionScript = completionScript;
        titleText.text = title;
        descriptionText.text = description;
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

}
