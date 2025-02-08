using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private float speed;
    private float oldSpeed = 0;
    private float health;
    private float fireRate;
    //public float fireRate2;
    private float damage;
    public float attackCooldown;
    private Transform currentRotation;
    private Transform targetPoint;
    public GameObject currentEnemy;
    public BoolVariable pause;

    public GameObject projectilePrefab; // Prefab del proiettile
    public float detectionRadius; // Raggio di rilevamento nemici

    private Quaternion initialRotation; // Variabile per memorizzare la rotazione iniziale
    public float rotationSpeed = 3f;

    public HealthBar healthBar;

    void Start()
    {
        // Salva la rotazione iniziale al momento dell'inizializzazione
        initialRotation = transform.rotation;
        oldSpeed = speed;
    }

    public void Initialize(EnemyData data, Transform target)
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
        if (!pause.Value)
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
    }

    private void MoveTowardsTarget()
    {
        if (targetPoint == null) return;

        //enemyMovement.MoveEnemy();

        Vector3 direction = (targetPoint.position - transform.position).normalized;


        direction.y = 0; // Se il movimento avviene su un piano orizzontale (X-Z)
        direction.x = 0;

        // Normalizza la direzione dopo aver modificato l'asse



        // Sposta l'unità lungo la direzione
        transform.position += direction * speed * Time.deltaTime;


    }

    private void SearchForEnemy()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, rotationSpeed * Time.deltaTime);

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var collider in hitColliders)
        {
            // Se trova una base, la priorizza come bersaglio
            if (collider.CompareTag("BaseA"))
            {
                currentEnemy = collider.gameObject;
                return;
            }
            else if (collider.CompareTag("Unit"))
            {
                currentEnemy = collider.gameObject;
                return;
            }
        }
    }

    private void AttackEnemy()
    {
        // Ferma il movimento mentre si attacca
        Vector3 directionToEnemy = currentEnemy.transform.position - transform.position;
        directionToEnemy.y = 0; // Mantieni l'orientamento orizzontale
        Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);


        // Ruota gradualmente verso il bersaglio
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Controlla il cooldown per l'attacco
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
            return; // Attendi il cooldown per attaccare di nuovo
        }

        if (currentEnemy == null)
        {
            Debug.LogWarning("Nessun nemico attuale da attaccare!");
            return;
        }

        //if (currentEnemy.CompareTag("BaseA"))
        //{
        //    Debug.Log($"Attaccando la base: {currentEnemy.name}");
        //    Applica danno direttamente alla base
        //    BaseHealth baseHealth = currentEnemy.GetComponent<BaseHealth>();
        //    if (baseHealth != null)
        //    {
        //        baseHealth.TakeDamage(damage);
        //    }
        //    else
        //    {
        //        Debug.LogError("Componente BaseBehavior mancante sulla base!");
        //    }
        //}
        //else
        if (projectilePrefab != null)
        {
            // Spara un proiettile verso l'unità nemica
            Debug.Log($"Sparo al nemico: {currentEnemy.name}");
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            if (projectile != null)
                projectile.GetComponent<EnemyProjectileBehavior>().Initialize(damage, currentEnemy.transform);
        }
        else
        {
            Debug.LogError("Prefab del proiettile non assegnato!");
        }

        // Imposta il cooldown per il prossimo attacco
        attackCooldown = 1f / fireRate;
    }

    private bool IsEnemyValid(GameObject enemy)
    {
        if (enemy == null) return false;

        float distance = Vector3.Distance(transform.position, enemy.transform.position);
        if (distance > detectionRadius) return false;

        EnemyBehaviour enemyBehavior = enemy.GetComponent<EnemyBehaviour>();

        if (enemyBehavior != null && enemyBehavior.health <= 0)
        {

            return false;
        }


        return true;
    }

    public void TakeDamage(float damage)
    {
        healthBar.TakeDamage(damage);

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

    public void PauseGameObject()
    {
        speed = 0;
    }

    public void ResumeGameObject()
    {
        speed = oldSpeed;
    }
}
