using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCam : MonoBehaviour
{
    public float forwardSpeed = 10;
    public float strafeSpeed = 10;

    public float lookRotationSpeed = 90;

    void Update()
    {
        float fwdAxis       = Input.GetAxis("Vertical");
        float strafeAxis    = Input.GetAxis("Horizontal");

        transform.localPosition +=  transform.forward * fwdAxis * forwardSpeed * Time.deltaTime + 
                                    transform.right * strafeAxis * strafeSpeed * Time.deltaTime;


        if(Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if(Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
        }


        if(Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            
            transform.Rotate(0, mouseX * lookRotationSpeed * Time.deltaTime, 0, Space.World);
            transform.Rotate(-mouseY * lookRotationSpeed * Time.deltaTime, 0, 0, Space.Self);
        }

    }
}
