using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Darken : MonoBehaviour {

    public Image mainImg;
    public bool hasStarted = false;
    

    public void Make(int i)
    {
        hasStarted = true;
        StartCoroutine(ChangeAlpha(i));
    }


    IEnumerator ChangeAlpha(int targetAlpha)
    {
        float currentAlpha = mainImg.color.a;
        float startAlpha = currentAlpha;

        float t = 0;

        for (int i = 0; i < 40; i++)
        {
            t += 1 / 40f;
            currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            mainImg.color = new Color(0, 0, 0, currentAlpha);
            yield return new WaitForSeconds(0.005f);
        }

        hasStarted = false;
        
    }
}
