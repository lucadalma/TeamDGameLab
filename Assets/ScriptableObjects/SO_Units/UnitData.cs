using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Game/Unit")]
public class UnitData : ScriptableObject
{
    public string unitName;
    public float health;          // Punti vita
    public float speed;           // Velocità di movimento
    public float fireRate;        // Frequenza di fuoco
    public float damage;          // Danno inflitto
    public int spawnCd;           // Cooldown per creare la stessa unità (solo test)
    public int unitID;            // ID dell unita per collegare il tasto allo spawn
    public int unitCount;
    public GameObject unitPrefab; // Prefab dell'unità
    public GameObject projectilePrefab; // Prefab dei proiettili

                                 
}