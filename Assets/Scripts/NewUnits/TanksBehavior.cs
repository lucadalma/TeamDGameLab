using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanksBehavior : MonoBehaviour
{
    public float speed;
    private float oldSpeed = 0;
    public float maxhealth;
    private float health;
    private float fireRate;
    private float damage;
    private float armor;
    public float attackCooldown;
    private Transform currentRotation;
    public Transform targetPoint;
    public GameObject currentEnemy;
    public bool isEnemy;

    public BoolVariable pause;

    public GameObject projectilePrefab; // Prefab del proiettile
    public float detectionRadius; // Raggio di rilevamento nemici

    private Quaternion initialRotation; // Variabile per memorizzare la rotazione iniziale
    public float rotationSpeed = 3f;

    public HealthBar healthBar;


    public EventManager EM;

    void Start()
    {

        health = maxhealth;


        if (EM == null)
            EM = FindObjectOfType<EventManager>();
        if (EM != null)
            PowerUp();





        // Salva la rotazione iniziale al momento dell'inizializzazione
        initialRotation = transform.rotation;
        oldSpeed = speed;
    }

    public void Initialize(UnitData data, Transform target)
    {
        speed = data.speed;
        maxhealth = data.health;
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

        if (health >= maxhealth)
        {
            health = maxhealth;
        }




    }

    private void MoveTowardsTarget()
    {
        if (targetPoint == null) return;

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
            if (collider.GetComponent<BaseScript>())
            {

                BaseScript EnemyBase = collider.GetComponent<BaseScript>();
                if (isEnemy != EnemyBase.isEnemy)
                {
                    currentEnemy = collider.gameObject;
                    return;
                }
            }

            // Altrimenti cerca un'unità nemica
            if (collider.GetComponent<TanksBehavior>())
            {
                TanksBehavior EnemyTank = collider.GetComponent<TanksBehavior>();
                if (isEnemy != EnemyTank.isEnemy)
                {
                    currentEnemy = collider.gameObject;
                    return;
                }

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
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

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

        //if (currentEnemy.CompareTag("BaseB"))
        //{
        //    Debug.Log($"Attaccando la base: {currentEnemy.name}");
        //    // Applica danno direttamente alla base
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
            projectile.GetComponent<ProjectileBehavior>().Initialize(damage, currentEnemy.transform, isEnemy);
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

        TanksBehavior enemyBehavior = enemy.GetComponent<TanksBehavior>();

        BaseHealth baseHealth = base.GetComponent<BaseHealth>();

        if (enemyBehavior != null && enemyBehavior.health <= 0 && baseHealth.health <= 0)
        {
            return false;
        }



        return true;
    }

    public void TakeDamage(float damage)
    {
        damage -= armor;
        Debug.Log(damage);

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



    private void PowerUp()
    {
        if (isEnemy == false)
        {
            if (gameObject.name == "Unit_Dart(Clone)")
            {
                EM.ForUnitDart(ref health,ref speed,ref maxhealth,ref armor, ref detectionRadius,ref damage,ref attackCooldown);
                Debug.Log(maxhealth);
            }
            if (gameObject.name == "Unit_Javeling(Clone)")
                EM.ForUnitJaveling(ref health, ref speed, ref maxhealth, ref armor, ref detectionRadius, ref damage, ref attackCooldown);
            if (gameObject.name == "Unit_Mace(Clone)")
                EM.ForUnitMace(ref health, ref speed, ref maxhealth, ref armor, ref detectionRadius, ref damage, ref attackCooldown);
            if (gameObject.name == "Unit_Gladius(Clone)")
                EM.ForUnitGladius(ref health, ref speed, ref maxhealth, ref armor, ref detectionRadius, ref damage, ref attackCooldown);

        }
    }
}
