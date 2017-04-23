using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{

    RectTransform rectTransform;
    public Image img;
    public TextPanel.TextGenerator generator;
    public ActionCenter actionCenter;
    public GameObject interactableObjects;
    public GameObject exit;
    

    bool startMessages = false;
    bool gameEnd = false;
    bool fadeoutEnd = false;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        img.color = Color.black;
        float height = Screen.height;
        float width = Screen.width * (1 - GameInfo.Constants.TextPanelPortionOfScreen);
        rectTransform.position = new Vector3(rectTransform.position.x - width, rectTransform.position.y, rectTransform.position.z);
        rectTransform.sizeDelta = new Vector2(width, height);
        string[] messages = new string[] {
            "You've been hit!", "We need to run!",
            "", "", "", "I think we've lost them.",
            "Now, now, lay down. You need to rest.",
            "We need to fix you up, and quick."
        };
        generator.NewMessage(messages);
    }

    void Update()
    {
        if(generator.messageQueue.Count == 0 && startMessages == false)
        {
            startMessages = true;
            StartCoroutine(FadeOut(0));
        }

        if (gameEnd)
        {
            if(Input.GetKeyDown(KeyCode.Space)){
                Application.Quit();
            }
        }
    }

    public void GameEnd()
    {
        generator.NewMessage("You stand up, with the help of your companion.");
        generator.NewMessage("You exit the watchtower.");
        StartCoroutine(FadeOut(1));
        StartCoroutine(LastMessages());
    }



    IEnumerator FadeOut(int id)
    {
        float startAlpha = img.color.a;
        float t = 0;
        for (int i = 0; i < 20; i++)
        {
            
            t += 1 / 20f;
            float alpha = Mathf.Lerp(startAlpha, id, t);
            img.color = new Color(0, 0, 0, alpha);
            yield return new WaitForSeconds(0.05f);
        }
        if (id == 1)
        {
            interactableObjects.SetActive(false);
            exit.GetComponent<Collider>().enabled = false;
            string[] endMessages = new string[]
            {"-------------------",
            "Game created by Scoutas, for Ludum Dare 38",
            "Inspiration: A Dark Room",
            "To quit the game, press Spacebar."
            };
            generator.NewMessage(endMessages);
            
        }
    }

    IEnumerator LastMessages()
    {
        while(generator.messageQueue.Count > 0)
        {
            yield return new WaitForSeconds(0.2f);
        }

        gameEnd = true;


    }
}