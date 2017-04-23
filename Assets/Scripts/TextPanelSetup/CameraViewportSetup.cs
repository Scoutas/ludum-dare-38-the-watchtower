using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInfo;

namespace TextPanelSetup
{
    public class CameraViewportSetup : MonoBehaviour
    {

        Camera mainCamera;

        void Awake()
        {

            mainCamera = GetComponent<Camera>();
            // Push the Viewport from the left side, by the ammount that the TextPanel would take up.
            mainCamera.rect = new Rect(Constants.TextPanelPortionOfScreen, 0, 1, 1);

        }

    }
}