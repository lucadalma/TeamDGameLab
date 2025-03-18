using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;
    private Transform target;
    private bool isEnemy;
    TanksBehavior Enemytank;
    BaseScript EnemyBase;

    public BoolVariable pause;

    public void Initialize(float damage, Transform target, bool isEnemy)
    {
        this.damage = damage;
        this.target = target;
        this.isEnemy = isEnemy;

        if (target.gameObject.GetComponent<TanksBehavior>())
        {
            Enemytank = target.gameObject.GetComponent<TanksBehavior>();
        }
        else if (target.gameObject.GetComponent<BaseScript>())
        {
            EnemyBase = target.gameObject.GetComponent<BaseScript>();
        }

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
        if (!pause.Value)
        {

            if (target == null)
            {
                // Debug.LogWarning("Il target del proiettile è scomparso!");
                //transform.Translate(Vector3.forward * speed * Time.deltaTime);
                Destroy(gameObject);
                return;

            }
            else
            {
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