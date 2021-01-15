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
        bool status=false;
        bool status2 =false;
        bool value = true;
        // log.questStatus.TryGetValue(closeQuest, out status2);
        log.questStatus.TryGetValue(openQuest, out status);
        if (status)
        {
            value = false;
        }
        log.questStatus.TryGetValue(closeQuest, out status2);
        if (status2)
        {
            value = true;
        }
        this.gameObject.GetComponent<Renderer>().enabled = value;
        this.gameObject.GetComponent<Collider>().enabled = value;
    }
}
