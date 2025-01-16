using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMap : MonoBehaviour
{
    [Header("Obj")]
    public GameObject Camera;
    public Camera m_Camera;

    [Header("SensCam")]
    public float rotationSpeed;


    [Header("MoveCam")]
    public float movespeed;
    public float maxDistance, minDistance;

    [Header("Zoom")]
    public float max;
    public float min;


    private Vector3 turn;
    private Vector3 move;



    void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.Confined;

        CameraMove();
        CameramanMove();
        Zoom();
    }

    private void CameraMove()
    {
        turn.y += Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        turn.y = Mathf.Clamp(turn.y, -90, 5);
        Camera.transform.localRotation = Quaternion.Euler(-turn.y, -53, 0);

    }

    void CameramanMove()
    {
        move = new Vector3(0f, 0f, Input.GetAxis("Vertical")) * 1;
        transform.position += (move * movespeed) * Time.deltaTime;

        if (transform.position.z > maxDistance)
        {
            transform.position = new Vector3(0, 0, maxDistance);
        }
        else if (transform.position.z < -minDistance)
        {
            transform.position = new Vector3(0, 0, -minDistance);
        }
    }

    void Zoom()
    {

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (m_Camera.fieldOfView < max)
                m_Camera.fieldOfView += 2;

        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (m_Camera.fieldOfView > min)
                m_Camera.fieldOfView -= 2;
        }
    }
}
