using UnityEngine;

public class EnemyProjectileBehavior : MonoBehaviour
{
    [SerializeField]
    public float speed;
    private float oldSpeed;
    [SerializeField]
    public float damage;
    private Transform target;

    private Vector3 point;


    private void Start()
    {
        oldSpeed = speed;
        point = target.position;
    }

    public void Initialize(float damage, Transform target)
    {

        this.target = target;

        if (target != null)
        {
            Debug.Log($"Proiettile inizializzato per colpire: {target.name} con danno: {damage}");
        }
        else
        {
            Debug.LogError("Il target del proiettile è nullo!");
        }
    }

    void Update()
    {
        if (target == null)
        {
            // Debug.LogWarning("Il target del proiettile è scomparso!");
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime);


        if (Vector3.Distance(transform.position, point) < 0.1f)
        {
            if (target.gameObject.CompareTag("BaseA"))
            {
                BaseHealth baseHealth = target.GetComponent<BaseHealth>();
                if (baseHealth != null)
                {
                    baseHealth.TakeDamage(damage);
                    Debug.Log($"Danno inflitto: {damage} a {baseHealth.name}");
                }
                else
                {
                    Debug.LogWarning("Il nemico non ha uno script UnitBehavior!");
                }
            }
            else if (target.gameObject.CompareTag("Unit"))
            {

                UnitBehavior unit = target.GetComponent<UnitBehavior>();
                if (unit != null)
                {
                    unit.TakeDamage(damage);
                    Debug.Log($"Danno inflitto: {damage} a {unit.name}");
                }
                else
                {
                    Debug.LogWarning("Il nemico non ha uno script UnitBehavior!");
                }
            }
            Destroy(gameObject);
        }
    }

    public void StopProjectile()
    {
        speed = 0;
    }

    public void ResumeProjectile()
    {
        speed = oldSpeed;
    }
}