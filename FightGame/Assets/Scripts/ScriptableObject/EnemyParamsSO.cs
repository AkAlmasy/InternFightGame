using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ellenfél paraméter sablon 
/// </summary>
[CreateAssetMenu(fileName = "EnemyParams", menuName = "Create EnemyParamsSO")]
public class EnemyParamsSO : ScriptableObject {

    public float MoveSpeed;

    public float AttackSpeed;

    public float AttackRange;

    public int Damage;

    public int Health;

    public int ManaWorth;

    public int CurrencyWorth;

    public int HealthGainPerLevel;

    public int GameObjectDestroyTime;

    public int DeathThrust;

    public GameObject Prefab;

    public int WaitBeforeThisType;

    public Color DeathColor;
}
