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

    private Vector3 direction;  // La direzione del proiettile

    private float lifetime = 5f;  // Tempo di vita del proiettile

    public void Initialize(float damage, Transform target, bool isEnemy)
    {
        this.damage = damage;
        this.target = target;
        this.isEnemy = isEnemy;

        // Imposta la direzione verso il target, se esiste
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

            direction = (target.position - transform.position).normalized;  // Direzione verso il target
        }
        else
        {
            // Se il target è null, il proiettile si muove in avanti
            direction = transform.forward; // Muove il proiettile in avanti
        }

        if (target != null)
        {
            Debug.Log($"Proiettile inizializzato per colpire: {target.name} con danno: {damage}");
        }
        else
        {
            Debug.Log("Il proiettile non ha un target, si muove comunque verso la sua direzione iniziale.");
        }
    }

    void Update()
    {
        if (!pause.Value)
        {
            // Movimento continuo del proiettile
            // transform.position += direction * speed * Time.deltaTime;

            transform.position += transform.forward * speed * Time.deltaTime;

            // Controllo del tempo di vita del proiettile
            lifetime -= Time.deltaTime;
            if (lifetime <= 0f)
            {
                Destroy(gameObject);  // Distrugge il proiettile se il tempo è scaduto
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<TanksBehavior>())
        {
            TanksBehavior unit = collision.gameObject.GetComponent<TanksBehavior>();
            // Se è un nemico (basato sulla variabile isEnemy)
            if (unit != null && isEnemy != unit.isEnemy)
            {
                unit.TakeDamage(damage);
                Debug.Log($"Danno inflitto: {damage} a {unit.name}");
                Destroy(gameObject);  // Distrugge il proiettile dopo aver inflitto danno
                return;
            }
        }
        else if (collision.gameObject.GetComponent<BaseHealth>())
        {
            BaseHealth enemyBase = collision.gameObject.GetComponent<BaseHealth>();
            // Se è un nemico (basato sulla variabile isEnemy)
            if (enemyBase != null && isEnemy != enemyBase.Base.isEnemy)
            {
                enemyBase.TakeDamage(damage);
                Debug.Log($"Danno inflitto: {damage} a {enemyBase.name}");
                Destroy(gameObject);  // Distrugge il proiettile dopo aver inflitto danno
                return;
            }
        }
    }
}
    
