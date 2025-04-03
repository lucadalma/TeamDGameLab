using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour
{
    [SerializeField] GameEvent evento;
    [SerializeField] BaseHealth health;
    [SerializeField] BoolVariable pause;

    public bool isEnemy;
    public Slider speedSlider;
    public int moveSpeed;
    private float speed;

    public List<int> hpThresholds;

    [Header("Retreat Settings")]
    public List<float> retreatDistances;
    public float retreatSpeed = 2f;
    public float acceleration = 1f;  // Nuova variabile per accelerare gradualmente

    private float currentSpeed = 0f; // Velocità attuale per la ritirata

    public bool isRetreating = false;
    public Vector3 retreatTarget;
    public int lastThresholdIndex = -1;

    private void OnValidate()
    {
        if (hpThresholds.Count != retreatDistances.Count)
        {
            while (hpThresholds.Count < retreatDistances.Count)
            {
                hpThresholds.Add(0);
            }

            while (hpThresholds.Count > retreatDistances.Count)
            {
                hpThresholds.RemoveAt(hpThresholds.Count - 1);
            }
        }
    }

    void Update()
    {
        if (isEnemy)
        {
            if (pause.Value == false)
            {
                if (isRetreating)
                {
                    MoveBackward();
                }

            }
        }

        if (speedSlider != null)
        {
            if (speedSlider.value > 0 && speedSlider.value < 0.9f)
            {
                speedSlider.value = 0;
            }
            else if (speedSlider.value < 0 && speedSlider.value > -0.9f)
            {
                speedSlider.value = 0;
            }
            speed = speedSlider.value;
            if (!pause.Value)
                transform.Translate(Vector3.forward * -(speed * moveSpeed) * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        evento.Raise();
    }

    void MoveBackward()
    {
        float distanceRemaining = Vector3.Distance(transform.position, retreatTarget);
        float totalDistance = retreatDistances[lastThresholdIndex];

        if (currentSpeed < retreatSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed, retreatSpeed);
        }

        float speedFactor = Mathf.Clamp01(distanceRemaining / totalDistance);
        float adjustedSpeed = currentSpeed * speedFactor;

        transform.position = Vector3.MoveTowards(transform.position, retreatTarget, adjustedSpeed * Time.deltaTime);

        if (distanceRemaining < 0.5f)
        {
            isRetreating = false;
            currentSpeed = 0f;
        }
    }



    public void StartRetreat(int index)
    {
        if (isRetreating) return;
        if (index >= retreatDistances.Count) return;

        isRetreating = true;
        retreatTarget = transform.position - transform.forward * retreatDistances[index];
        lastThresholdIndex = index;
        currentSpeed = 0f;

        Debug.Log($"Retreating {retreatDistances[index]} units!");
    }

}