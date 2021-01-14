using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    Image bar;
    PlayerCombat playerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerCombat>();
        bar = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.upgrades["Armor"])
        {
            SetRight(bar.rectTransform, (100 - (playerScript.health) / 2));
        }
        else
        {
            SetRight(bar.rectTransform, (100 - playerScript.health));
        }
    }
    public static void SetRight(RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }
}
