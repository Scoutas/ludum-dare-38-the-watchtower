using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TextPanel
{

    public class TextGenerator : MonoBehaviour
    {
        RectTransform rectTransform;
        public GameObject textPrefab;
        public ActionCenter actionCenter;
        float animationJump;
        public bool currentlyAnimatingText = false;

        public Queue<string> messageQueue;
       
        void Start()
        {
            if (messageQueue == null)
            {
                messageQueue = new Queue<string>();
            }
            rectTransform = GetComponent<RectTransform>();
            animationJump = textPrefab.GetComponent<RectTransform>().sizeDelta.y + GetComponent<VerticalLayoutGroup>().spacing;
            for (int i = 0; i < GameInfo.Constants.maxMessages; i++)
            {
                GameObject go = Instantiate(textPrefab);
                go.name = "Message number: " + (i + 1);
                go.transform.parent = this.transform;
                go.GetComponent<Text>().text = "";
                if(i == GameInfo.Constants.maxMessages - 1)
                {
                    go.GetComponent<Text>().color = new Color(0, 0, 0, 0);
                }
            }
        }

        void Update()
        {
            if(currentlyAnimatingText == false && messageQueue.Count > 0)
            {
                StartCoroutine(AnimateText(messageQueue.Dequeue()));
            }
        }

        public void NewMessage(string message)
        {
            messageQueue.Enqueue(message);
        }

        public void NewMessage(string[] messages)
        {
            foreach (string str in messages) {
                if(messageQueue == null)
                {
                    messageQueue = new Queue<string>();
                }
                messageQueue.Enqueue(str);
            }
        }

        IEnumerator AnimateText(string message)
        {
            currentlyAnimatingText = true;
            actionCenter.actionIsHappening = true;
            Vector3 startVector = rectTransform.position + Vector3.up * animationJump;
            Vector3 endVector = rectTransform.position;
            Color startColor = Color.white;
            Color endColor = new Color(0, 0, 0, 0);
            float iterations = 50;

            MoveText(startVector);

            yield return new WaitForSeconds(0.1f);

            float t = 0;
            for (int i = 0; i < iterations; i++)
            {
                actionCenter.actionIsHappening = true;
                t += (1 / iterations);
                rectTransform.position = Vector3.Lerp(startVector, endVector, t);
                transform.GetChild(transform.childCount - 1).GetComponent<Text>().color = Color.Lerp(startColor, endColor, t);

                yield return new WaitForSeconds(0.01f);

            }
            
            for (int i = 0; i < message.Length; i++)
            {
                actionCenter.actionIsHappening = true;
                transform.GetChild(0).GetComponent<Text>().text += message[i];
                yield return new WaitForSeconds(0.01f);
            }

            actionCenter.actionIsHappening = false;
            currentlyAnimatingText = false;
        }

        void MoveText(Vector3 startVector)
        {
            for (int i = (transform.childCount - 1); i > 0; i--)
            {

                transform.GetChild(i).GetComponent<Text>().text = transform.GetChild(i - 1).GetComponent<Text>().text;
                if (i == 1)
                {
                    transform.GetChild(i - 1).GetComponent<Text>().text = "";
                }
            }

            rectTransform.position = startVector;
            transform.GetChild(transform.childCount - 1).GetComponent<Text>().color = Color.white;
        }



    }
}