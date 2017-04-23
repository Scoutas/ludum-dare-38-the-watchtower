using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IObject;

public class MouseController : MonoBehaviour {

    public LayerMask layerMask;
    public GameObject buttonPrefab;
    public Transform optionPanel;
    public ActionCenter actionCenter;

    [HideInInspector] public Camera mainCam;
    [HideInInspector] public Transform selectedObject;
    [HideInInspector] public Transform hardSelectedObject;
    

    void Start()
    {
        mainCam = Camera.main;
    }

    public void ManualUnselect() {
        for (int i = 0; i < optionPanel.childCount; i++)
        {
            Destroy(optionPanel.GetChild(i).gameObject);
        }
        hardSelectedObject = null;
    }

    void Update()
    {
        if (actionCenter.actionIsHappening == false)
        {
            RaycastHit hitInfo;
            Ray mainMouseRay = mainCam.ScreenPointToRay(Input.mousePosition);
            selectedObject = null;
            bool objectHit = Physics.Raycast(mainMouseRay.origin, mainMouseRay.direction, out hitInfo, 50f, layerMask);
            if (objectHit)
            {

                if (hardSelectedObject == null)
                {
                    selectedObject = hitInfo.transform;

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        hardSelectedObject = selectedObject;
                        hardSelectedObject.GetComponent<InteractableObject>().SetupButtons(buttonPrefab, optionPanel);
                    }

                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        if (hitInfo.transform == hardSelectedObject)
                        {
                            for (int i = 0; i < optionPanel.childCount; i++)
                            {
                                Destroy(optionPanel.GetChild(i).gameObject);
                            }
                            hardSelectedObject = null;

                        }
                    }
                }


            }
        }
    }
	
}
