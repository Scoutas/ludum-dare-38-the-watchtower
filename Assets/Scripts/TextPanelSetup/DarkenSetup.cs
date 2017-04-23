using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkenSetup : MonoBehaviour {

    RectTransform darkenTransform;

    void Awake()
    {
        darkenTransform = GetComponent<RectTransform>();
        float height = Screen.height;
        float width = Screen.width * (1 - GameInfo.Constants.TextPanelPortionOfScreen);
        darkenTransform.position = new Vector3(darkenTransform.position.x - width, darkenTransform.position.y, darkenTransform.position.z);
        darkenTransform.sizeDelta = new Vector2(width, height);
    }
}
