using UnityEngine;
using UnityEngine.UI;

public class DamageFlash : MonoBehaviour
{
    [Header("UI Setup")]
    public Image damageImage;  // UI Image rossa a schermo intero

    [Header("Flash Settings")]
    public float flashDuration = 0.5f;
    public float maxAlpha = 0.8f;         // Intensità massima dell'effetto
    public float maxDamage = 100f;        // Danno massimo atteso

    private float currentAlpha = 0f;
    private float timer = 0f;

    void Start()
    {
        if (damageImage != null)
        {
            damageImage.color = new Color(1, 0, 0, 0); // rosso trasparente
        }
    }


    /// <param name="damage">Quantità di danno (float)</param>
    public void Flash(float damage)
    {
        // Calcola opacità proporzionale al danno, limitata da maxAlpha
        float alpha = Mathf.Clamp01(damage / maxDamage) * maxAlpha;
        currentAlpha = alpha;

        damageImage.color = new Color(1, 0, 0, currentAlpha);
        timer = flashDuration;
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            float t = 1f - (timer / flashDuration);
            float fadedAlpha = Mathf.Lerp(currentAlpha, 0f, t);
            damageImage.color = new Color(1, 0, 0, fadedAlpha);
        }
    }
}