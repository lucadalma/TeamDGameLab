using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField]
    private float speed = 400f;
    private float damage;
    private Transform target;

    public void Initialize(float damage, Transform target)
    {
        this.damage = damage;
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
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            Debug.Log($"Proiettile ha colpito: {target.name}");
            EnemyHealth enemy = target.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log($"Danno inflitto: {damage} a {enemy.name}");
            }
            else
            {
                Debug.LogWarning("Il nemico non ha uno script UnitBehavior!");
            }
            Destroy(gameObject);
        }
    }
}