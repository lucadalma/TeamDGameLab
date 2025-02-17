using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMap : MonoBehaviour
{
    [Header("Obj")]
    public Camera Camera;

    [Header("Y rotation")]
    public float speedYRotation;


    [Header("MoveCam")]
    public float moveSpeedY;
    public float maxHeight;
    public float minHeight;

    [Header("Zoom")]
    public float max;
    public float min;

    private Transform localTrans;


    private void Start()
    {
        localTrans = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        CameramanMove();
        Zoom();
    }

    void CameramanMove()
    {

        if (Input.GetMouseButton(1))
            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * speedYRotation);
        
        
        float movementY = Input.GetAxis("CameraYMove");
        float movementZ = -Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(0, movementY, movementZ) * moveSpeedY;
        float newY = Mathf.Clamp(transform.position.y + movement.y * Time.deltaTime, minHeight, maxHeight);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z) + movement * Time.deltaTime;


    }

    void Zoom()
    {

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.fieldOfView < max)
                Camera.fieldOfView += 5;

        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.fieldOfView > min)
                Camera.fieldOfView -= 5;
        }
    }
}
