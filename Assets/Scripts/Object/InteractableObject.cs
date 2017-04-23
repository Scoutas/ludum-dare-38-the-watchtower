using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IObject
{
    public class InteractableObject : MonoBehaviour, IInteractable
    {

        public string objectName;
        [HideInInspector] public ActionCenter actionCenter;
        [HideInInspector] public TextPanel.TextGenerator generator;
        [HideInInspector] public MouseController mouseController;

        void Awake()
        {
            actionCenter = FindObjectOfType<ActionCenter>();
            generator = FindObjectOfType<TextPanel.TextGenerator>();
            mouseController = FindObjectOfType<MouseController>();
        }

        public virtual void SetupButtons(GameObject prefab, Transform parent)
        {
            throw new NotImplementedException("You did not override 'SetupButtons' on " + objectName);
        }

        public void GenerateButton(GameObject prefab, Transform parent, string buttonName, UnityEngine.Events.UnityAction function)
        {
            GameObject newButton = Instantiate(prefab);
            newButton.transform.parent = parent;
            newButton.GetComponentInChildren<Text>().text = buttonName;
            newButton.GetComponent<Button>().onClick.AddListener(function);
        }
    }

}