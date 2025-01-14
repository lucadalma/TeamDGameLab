using UnityEngine;

[CreateAssetMenu(fileName = "NewUnit", menuName = "Game/Unit")]
public class UnitData : ScriptableObject
{
    public string unitName;
    public GameObject unitPrefab; // Prefab dell'unità
    public float speed;           // Velocità di movimento
    public float health;          // Punti vita
    public float fireRate;        // Frequenza di fuoco
    public GameObject projectilePrefab; // Prefab dei proiettili
    public float damage;          // Danno inflitto
}