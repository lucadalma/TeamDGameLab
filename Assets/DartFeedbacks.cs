using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartFeedbacks : MonoBehaviour
{
    [Header("FEEL Feedbacks")]
    public MMFeedbacks fireFeedback; // Feedback per lo sparo (rinculo, fumo, suono)
    public MMFeedbacks landFeedback; // Feedback per l'atterraggio (polvere)

    [Header("Sparo")]
    public Transform firePoint; // Punto da cui parte il proiettile



    [Header("Atterraggio")]
    public LayerMask groundLayer; // Strato del terreno per il rilevamento dell'atterraggio
    private bool hasLanded = false; // Controlla se ha già toccato terra

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Tasto per sparare
        {
            Fire();
        }
    }

    void Fire()
    {
        if (fireFeedback != null)
        {
            fireFeedback.PlayFeedbacks(); // Attiva rinculo e fumo
        }

        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0) // Controlla se tocca il terreno
        {
            if (!hasLanded)
            {
                hasLanded = true;
                if (landFeedback != null)
                {
                    landFeedback.PlayFeedbacks(); // Genera polvere all'atterraggio
                }
            }
        }
    }
}
