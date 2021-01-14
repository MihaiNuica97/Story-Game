using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDoor : MonoBehaviour
{
    QuestLog log;
    public string openQuest;
    public string closeQuest;
    // Start is called before the first frame update
    void Start()
    {
        log = GameObject.Find("Quest Tracker").GetComponent<QuestLog>();
    }

    // Update is called once per frame
    void Update()
    {
        bool status;
        bool status2;
        log.questStatus.TryGetValue(closeQuest, out status2);
        if (!status2) { 
        log.questStatus.TryGetValue(openQuest, out status) ;
            if (status)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
