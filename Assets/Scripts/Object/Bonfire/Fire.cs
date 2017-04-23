using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {


    public GameObject smallFire;
    public GameObject mediumFire;
    public GameObject largeFire;

    public void SetLight(int logCount) {
        if(logCount < 2)
        {
            smallFire.SetActive(true);
            mediumFire.SetActive(false);
            largeFire.SetActive(false);
            return;
        }
        if (logCount < 4)
        {
            smallFire.SetActive(false);
            mediumFire.SetActive(true);
            largeFire.SetActive(false);
            return;
        }

        if (logCount < 6)
        {
            smallFire.SetActive(false);
            mediumFire.SetActive(false);
            largeFire.SetActive(true);
            return;
        }
    }
}
