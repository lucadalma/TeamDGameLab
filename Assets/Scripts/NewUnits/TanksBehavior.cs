using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Animations;

public enum Lane
{
    Lane1,
    Lane2,
    Lane3
}

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

    public Lane lane;

    public BoolVariable pause;

    public GameObject projectilePrefab; // Prefab del proiettile
    public float detectionRadius; // Raggio di rilevamento nemici

    private Quaternion initialRotation; // Variabile per memorizzare la rotazione iniziale
    public float rotationSpeed = 3f;

    public HealthBar healthBar;


    public EventManager EM;
    public EnemyBase_EventManager EBEM;


    public MMF_Player feedbacks;
    public MMF_Player explosion;
    public GameObject explosionPrefab;




    [SerializeField] private Transform cannonTransform;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Transform firePointTransform;

    public float recoilDistance = 0.3f;
    public float recoilDuration = 0.1f;
    public float returnDuration = 0.2f;

    private Vector3 _initialLocalPosition;


    void Start()
    {


        health = maxhealth;


        if (EM == null)
            EM = FindObjectOfType<EventManager>();
        if (EBEM == null)
            EBEM = FindObjectOfType<EnemyBase_EventManager>();
        if (EM != null || EBEM != null)
            PowerUp();




        detectionRadius *= 55;



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
        _initialLocalPosition = transform.localPosition;

        if (isEnemy == false)
            OverDriver();
        if (isEnemy == true)
            EMP();


        if (!pause.Value)
        {

            if (currentEnemy == null || !IsEnemyValid(currentEnemy))
            {

                if (emp == false)
                {
                    // Nessun nemico valido, continua a muoverti
                    MoveTowardsTarget();
                    SearchForEnemy();
                }

            }
            else
            {
                if (emp == false)
                {
                    // Attacca il nemico
                    AttackEnemy();
                }

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
        transform.position += direction * (speed * 50) * Time.deltaTime;


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
                    if(lane == EnemyTank.lane) 
                    {
                        currentEnemy = collider.gameObject;
                        return;
                    }
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
            feedbacks.PlayFeedbacks();
            PlayRecoil();

            // Spara un proiettile verso l'unità nemica
            Debug.Log($"Sparo al nemico: {currentEnemy.name}");
            //GameObject projectile = Instantiate(projectilePrefab, firePointTransform.position, Quaternion.identity);
            //projectile.GetComponent<ProjectileBehavior>().Initialize(damage, currentEnemy.transform, isEnemy);

            // Instanzia proiettile
            GameObject projectile = Instantiate(projectilePrefab, firePointTransform.position, Quaternion.identity);

            //Calcola direzione orizzontale(ignora altezza se necessario)
            Vector3 targetPos = currentEnemy.transform.position;
            targetPos.y = firePointTransform.position.y; // Ignora altezza

            Vector3 direction = (targetPos - firePointTransform.position).normalized;

            // Ruota il proiettile per puntare verso il bersaglio
            projectile.transform.rotation = Quaternion.LookRotation(direction);

            // Inizializza il comportamento
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


        if (explosionPrefab != null)
        {
            // Istanzia l'effetto esplosione alla posizione corrente
            GameObject fx = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            fx.transform.localScale = Vector3.one * 2f;
            // Opzionale
            Destroy(fx, 2f);
        }
        if (gameObject.name.Contains("Dart"))
        {
            AudioManager.Instance.Play(AudioManager.SoundType.DartExplosion);
        }
        else if (gameObject.name.Contains("Gladius"))
        {
            AudioManager.Instance.Play(AudioManager.SoundType.GladiusExplosion);
        }
        else if (gameObject.name.Contains("Javelin"))
        {
            AudioManager.Instance.Play(AudioManager.SoundType.JavelinExplosion);
        }
        else if (gameObject.name.Contains("Mace"))
        {
            AudioManager.Instance.Play(AudioManager.SoundType.MaceExplosion);
        }



        Destroy(gameObject);
    }



    private void PowerUp()
    {
        if (isEnemy == false)
        {
            if (gameObject.name == "Unit_Dart(Clone)")
            {
                EM.ForUnitDart(ref health, ref speed, ref maxhealth, ref armor, ref detectionRadius, ref damage, ref attackCooldown);
                Debug.Log(maxhealth);
            }
            if (gameObject.name == "Unit_Javeling(Clone)")
                EM.ForUnitJaveling(ref health, ref speed, ref maxhealth, ref armor, ref detectionRadius, ref damage, ref attackCooldown);
            if (gameObject.name == "Unit_Mace(Clone)")
                EM.ForUnitMace(ref health, ref speed, ref maxhealth, ref armor, ref detectionRadius, ref damage, ref attackCooldown);
            if (gameObject.name == "Unit_Gladius(Clone)")
                EM.ForUnitGladius(ref health, ref speed, ref maxhealth, ref armor, ref detectionRadius, ref damage, ref attackCooldown);

        }
        else 
        {

            if (gameObject.name == "Unit_Dart(Clone)")
            {
                EBEM.ForUnitDart(ref health, ref speed, ref maxhealth, ref armor, ref detectionRadius, ref damage, ref attackCooldown);
                Debug.Log(maxhealth);
            }
            if (gameObject.name == "Unit_Javeling(Clone)")
                EBEM.ForUnitJaveling(ref health, ref speed, ref maxhealth, ref armor, ref detectionRadius, ref damage, ref attackCooldown);
            if (gameObject.name == "Unit_Mace(Clone)")
                EBEM.ForUnitMace(ref health, ref speed, ref maxhealth, ref armor, ref detectionRadius, ref damage, ref attackCooldown);
            if (gameObject.name == "Unit_Gladius(Clone)")
                EBEM.ForUnitGladius(ref health, ref speed, ref maxhealth, ref armor, ref detectionRadius, ref damage, ref attackCooldown);
        }


    }



    public bool overdrive, emp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OverDrive") && isEnemy == false)
        {
            overdrive = true;
        }

        if (other.CompareTag("EMP") && isEnemy == true)
        {
            emp = true;
        }

        if (other.CompareTag("Missile"))
        {
            TakeDamage(50);
            Debug.Log(health);
        }
    }

    float timers;
    float reloadOD;
    private void OverDriver()
    {
        attackCooldown = attackCooldown + reloadOD;
        if (overdrive == true)
        {
            timers += 1 * Time.deltaTime;
            reloadOD = 0.5f;


            if (timers >= 20)
            {
                overdrive = false;
            }

        }
        else if (overdrive == false)
        {
            timers = 0;
            reloadOD = 0;
        }
    }

    void EMP()
    {
        if (emp == true)
        {
            timers += 1 * Time.deltaTime;


            if (timers >= 30)
            {
                emp = false;
            }

        }
        else if (emp == false)
        {
            timers = 0;
        }
    }

    public void PlayRecoil()
    {
        // Ferma eventuali animazioni in corso
        cannonTransform.DOKill();
        bodyTransform.DOKill();

        Vector3 currentLocalPosition = cannonTransform.localPosition;
        Vector3 currentBodyLocalPosition = bodyTransform.localPosition;

        // Calcola il punto di rinculo basato sulla posizione attuale
        Vector3 recoilTarget = currentLocalPosition + Vector3.forward * recoilDistance;
        Vector3 recoilBodyTarget = currentBodyLocalPosition + Vector3.back * recoilDistance;

        // Esegui rinculo → ritorno
        cannonTransform.DOLocalMove(recoilTarget, recoilDuration).SetEase(Ease.OutQuad);
        bodyTransform.DOLocalMove(recoilBodyTarget, recoilDuration).SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                cannonTransform.DOLocalMove(currentLocalPosition, returnDuration)
                    .SetEase(Ease.InOutQuad);

                bodyTransform.DOLocalMove(currentBodyLocalPosition, returnDuration)
                    .SetEase(Ease.InOutQuad);
            });
    }


}

