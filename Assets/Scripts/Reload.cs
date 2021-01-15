using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    CanvasGroup canvas;
    PlayerCombat player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCombat>();
        canvas = gameObject.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.health == 0)
        {
            canvas.alpha = 1;
            canvas.interactable = true;
        }
    }
    public void Respawn()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
