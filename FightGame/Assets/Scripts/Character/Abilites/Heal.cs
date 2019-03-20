using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Heal képesség.
/// </summary>
public class Heal : MonoBehaviour {

    [SerializeField]
    private CounterManager PlayerHealth;
    [SerializeField]
    private CounterManager ManaCounter;


    public void Healing(int healAmount, int manaCost)
    {
        if (ManaCounter.Number >= manaCost)
        {
            PlayerHealth.AddNumber(healAmount);
            
            ManaCounter.SubNumber(manaCost);
        }
    }

}
