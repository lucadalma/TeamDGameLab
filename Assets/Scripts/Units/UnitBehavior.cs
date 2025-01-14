using UnityEngine;

public class UnitBehavior : MonoBehaviour
{
    private float speed;
    private float health;
    private float fireRate;
    private float damage;
    private float attackCooldown;
    private Transform targetPoint;
    private GameObject currentEnemy;

    public GameObject projectilePrefab; // Prefab del proiettile
    public float detectionRadius = 15f; // Raggio di rilevamento nemici

    public void Initialize(UnitData data, Transform target)
    {
        speed = data.speed;
        health = data.health;
        fireRate = data.fireRate;
        damage = data.damage;
        projectilePrefab = data.projectilePrefab;
        targetPoint = target;
    }

    void Update()
    {
        if (currentEnemy == null || !IsEnemyValid(currentEnemy))
        {
            // Nessun nemico valido, continua a muoverti
            MoveTowardsTarget();
            SearchForEnemy();
        }
        else
        {
            // Attacca il nemico
            AttackEnemy();
        }
    }

    private void MoveTowardsTarget()
    {
        if (targetPoint == null) return;

        Vector3 direction = (targetPoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void SearchForEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                currentEnemy = collider.gameObject;
                break;
            }
        }
    }

    private void AttackEnemy()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
            return;
        }

        if (currentEnemy == null)
        {
            Debug.LogWarning("Nessun nemico attuale da attaccare!");
            return;
        }

        if (projectilePrefab != null)
        {
            Debug.Log($"Sparo al nemico: {currentEnemy.name}");
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<ProjectileBehavior>().Initialize(damage, currentEnemy.transform);
        }
        else
        {
            Debug.LogError("Prefab del proiettile non assegnato!");
        }

        attackCooldown = 1f / fireRate; // Tempo di ricarica basato su fire rate
    }

    private bool IsEnemyValid(GameObject enemy)
    {
        if (enemy == null) return false;

        float distance = Vector3.Distance(transform.position, enemy.transform.position);
        if (distance > detectionRadius) return false;

        UnitBehavior enemyBehavior = enemy.GetComponent<UnitBehavior>();
        if (enemyBehavior != null && enemyBehavior.health <= 0) return false;

        return true;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}