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



    [Header("MoveCam")]
    public float moveSpeedX;
    public float moveSpeedY;
    public float maxRotateY;
    public float minRotateY;
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
        float rotationY = -Input.GetAxis("Horizontal");
        float rotationX = Input.GetAxis("Vertical");

        Vector3 eulerAngle = localTrans.rotation.eulerAngles;

        eulerAngle.y = (eulerAngle.y > 180) ? eulerAngle.y - 360 : eulerAngle.y;

        eulerAngle.y += rotationY * Time.deltaTime * moveSpeedX;

        eulerAngle.y = Mathf.Clamp(eulerAngle.y, minRotateY, maxRotateY);

        Vector3 movement = new Vector3(0, rotationX, 0) * moveSpeedY;
        movement = transform.TransformDirection(movement);
        float newY = Mathf.Clamp(transform.position.y + movement.y * Time.deltaTime, minHeight, maxHeight);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z) + movement * Time.deltaTime;

        localTrans.rotation = Quaternion.Euler(eulerAngle);
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
