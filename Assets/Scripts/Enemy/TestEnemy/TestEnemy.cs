using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{

    public float speed;


    public GameObjectList enemyList;
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void PauseGameObject() 
    {
        speed = 0;
    }

    public void ResumeGameObject() 
    {
        speed = 50f;
        Debug.Log(speed);
    }

    private void Awake()
    {
        enemyList.List.Add(gameObject);
    }
    private void OnDestroy()
    {
        enemyList.List.Remove(gameObject);

    }

}
