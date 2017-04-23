using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Controllers
{
    public class CameraController : MonoBehaviour
    {

        public float cameraPanSpeed = 15f;
        float xRot;
        float yRot;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Confined;
            xRot = transform.rotation.eulerAngles.x;
            yRot = transform.rotation.eulerAngles.y;
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                xRot += Input.GetAxis("Mouse Y") * cameraPanSpeed * Time.deltaTime;
                yRot -= Input.GetAxis("Mouse X") * cameraPanSpeed * Time.deltaTime;
            }

            xRot = Mathf.Clamp(xRot, -50, 50);
            yRot = Mathf.Clamp(yRot, -55, 55);

            transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        }
    }
}