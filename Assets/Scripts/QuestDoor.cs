using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDoor : MonoBehaviour
{
    QuestLog log;
    public string questTitle;
    // Start is called before the first frame update
    void Start()
    {
        log = GameObject.Find("Quest Tracker").GetComponent<QuestLog>();
    }

    // Update is called once per frame
    void Update()
    {
        bool status;
        log.questStatus.TryGetValue(questTitle, out status);
        if (status)
        {
            gameObject.SetActive(false);
        }
    }
}
