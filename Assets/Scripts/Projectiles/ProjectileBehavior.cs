using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;
    private Transform target;
    private bool isEnemy;
    private TanksBehavior Enemytank;
    private BaseScript EnemyBase;

    public BoolVariable pause;

    private Vector3 direction;  // Vettore di direzione del proiettile
    private float lifetime = 5f; // Durata prima che il proiettile venga distrutto

    public void Initialize(float damage, Transform target, bool isEnemy)
    {
        this.damage = damage;
        this.target = target;
        this.isEnemy = isEnemy;

        // Se il target è nullo, impostiamo una direzione predefinita
        if (target != null)
        {
            if (target.gameObject.GetComponent<TanksBehavior>())
            {
                Enemytank = target.gameObject.GetComponent<TanksBehavior>();
            }
            else if (target.gameObject.GetComponent<BaseScript>())
            {
                EnemyBase = target.gameObject.GetComponent<BaseScript>();
            }
            direction = (target.position - transform.position).normalized;  // Calcoliamo la direzione
        }
        else
        {
            direction = transform.forward;  // Se il target è nullo, usiamo la direzione predefinita
        }

        Debug.Log($"Proiettile inizializzato per colpire: {target?.name ?? "nessuno"} con danno: {damage}");
    }

    void Update()
    {
        if (!pause.Value)
        {
            if (target == null)
            {
                // Movimento verso la direzione se il target è nullo
                transform.Translate(direction * speed * Time.deltaTime);
                lifetime -= Time.deltaTime;

                // Se il proiettile ha superato il tempo di vita, distruggilo
                if (lifetime <= 0)
                {
                    Destroy(gameObject);
                }

                // Esegui il Raycast per verificare se ci sono nemici lungo il percorso del proiettile
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction, out hit, speed * Time.deltaTime))
                {
                    // Verifica se il colpo ha colpito un Tank o una Base
                    TanksBehavior hitTank = hit.collider.GetComponent<TanksBehavior>();
                    BaseScript hitBase = hit.collider.GetComponent<BaseScript>();

                    if (hitTank != null && hitTank.isEnemy != isEnemy) // Se è un nemico e non è il nostro
                    {
                        // Se è un Tank nemico, infliggi danno
                        BaseHealth enemyHealth = hitTank.GetComponent<BaseHealth>();
                        if (enemyHealth != null)
                        {
                            enemyHealth.TakeDamage(damage);
                            Debug.Log($"Danno inflitto a {hit.collider.name}: {damage}");
                        }
                    }
                    else if (hitBase != null && hitBase.isEnemy != isEnemy) // Se è una Base nemica
                    {
                        // Se è una Base nemica, infliggi danno
                        BaseHealth baseHealth = hitBase.GetComponent<BaseHealth>();
                        if (baseHealth != null)
                        {
                            baseHealth.TakeDamage(damage);
                            Debug.Log($"Danno inflitto alla Base {hit.collider.name}: {damage}");
                        }
                    }
                }
                return;
            }
            else
            {
                // Se il target esiste, muovi verso di esso
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }

            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                if (EnemyBase)
                {
                    if (isEnemy != EnemyBase.isEnemy)
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
                }
                else if (Enemytank)
                {
                    if (isEnemy != Enemytank.isEnemy)
                    {
                        TanksBehavior unit = target.GetComponent<TanksBehavior>();
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
                }
                Destroy(gameObject);
            }
        }
    }
}
