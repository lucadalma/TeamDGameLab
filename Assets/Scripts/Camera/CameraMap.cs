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
    public float movespeed;
    public float maxRotate;
    public float minRotate;

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

        Vector3 eulerAngle = localTrans.rotation.eulerAngles;

        eulerAngle.y = (eulerAngle.y > 180) ? eulerAngle.y - 360 : eulerAngle.y;

        eulerAngle.y += rotationY * Time.deltaTime * movespeed;

        eulerAngle.y = Mathf.Clamp(eulerAngle.y, minRotate, maxRotate);

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
