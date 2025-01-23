using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Game/Enemy")]
public class EnemyData : ScriptableObject
{
    public string unitName;
    public float health;          // Punti vita
    public float speed;           // Velocit� di movimento
    public float fireRate;        // Frequenza di fuoco
    public float damage;          // Danno inflitto
    public int spawnCd;           // Cooldown per creare la stessa unit� (solo test)
    public int unitID;            // ID dell unita per collegare il tasto allo spawn
    public GameObject unitPrefab; // Prefab dell'unit�
    public GameObject projectilePrefab; // Prefab dei proiettili

}
