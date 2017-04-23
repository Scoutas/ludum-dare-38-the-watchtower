using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Factories;

namespace IObject
{
    public class SelectionIndicator : MonoBehaviour
    {

        public MouseController mouseController;
        public GameObject optionPanel;

        void Update()
        {

            if (mouseController.selectedObject != null)
            {
                ToggleOptionPanel(false);
                Toggle(1);
                DrawSelector(mouseController.selectedObject);
            }
            

            if (mouseController.hardSelectedObject != null)
            {
                ToggleOptionPanel(true);
                Toggle(1);
                DrawSelector(mouseController.hardSelectedObject);
            }


            else if (mouseController.selectedObject == null && mouseController.selectedObject == null)
            {
                ToggleOptionPanel(false);
                Toggle(0);
            }
        }

        void DrawSelector(Transform selectedObject)
        {
            MinMaxRect farthestCorners = SelectionIndicatorFactory.GetFarthestCorners(selectedObject.transform, mouseController);
            RectTransform rt = GetComponent<RectTransform>();
            rt.position = new Vector2(farthestCorners.minX, farthestCorners.minY);
            rt.sizeDelta = new Vector2(farthestCorners.maxX - farthestCorners.minX, farthestCorners.maxY - farthestCorners.minY);
            GetComponentInChildren<Text>().text = selectedObject.GetComponent<InteractableObject>().objectName;
        }

        void ToggleOptionPanel(bool toggle) { optionPanel.SetActive(toggle); }

        void Toggle(int toggle)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<CanvasRenderer>().SetAlpha(toggle);
            }
        }

    }
}