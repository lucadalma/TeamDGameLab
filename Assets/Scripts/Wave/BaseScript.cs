using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour
{
    [SerializeField]
    GameEvent evento;


    public Slider speedSlider;
    public int moveSpeed;
    private float speed;

    void Update()
    {
        if (speedSlider != null)
        {
            if (speedSlider.value > 0 && speedSlider.value < 0.9f)
            {
                speedSlider.value = 0;
            }
            else if(speedSlider.value < 0 && speedSlider.value > -0.9f)
            {
                speedSlider.value = 0;
            }
            speed = speedSlider.value;
            transform.Translate(Vector3.forward * -(speed * moveSpeed) * Time.deltaTime);

        }
    }

    private void OnDestroy()
    {
        evento.Raise();
    }
}
