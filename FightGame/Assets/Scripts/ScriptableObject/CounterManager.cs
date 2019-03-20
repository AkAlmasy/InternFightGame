using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



/// <summary>
/// Különböző értékekhez (amikhez egyszerre több helyről kell elérni) ScriptableObject.
/// </summary>
[CreateAssetMenu(fileName = "CounterManager", menuName = "Create CounterManager")]
public class CounterManager : ScriptableObject {

    /// <summary>
    /// Alapértelmezett érték változtató
    /// </summary>
    private int _number;
    [SerializeField]
    private int IncreaseNumber;
    /// <summary>
    /// Maximum érték az adott dolognak (gold/mana/hp)
    /// </summary>
    [SerializeField]
    private int maximumValue;


    public int Number
    {
        private set
        {
            _number = value;
            ChangeDataEvent.Invoke();
        }
        get
        {
            return _number;
        }
    }

    public int MaximumValue
    {
        get
        {
            return maximumValue;
        }
    }

    public UnityEvent ChangeDataEvent;

    public void Reset()
    {
        _number = 0;
    }
    public void Reset(int number)
    {
        _number = number;
    }

    public void AddNumber(int number)
    {
        Number = Mathf.Clamp(Number + number, 0, maximumValue);
    }

    public void AddNumber()
    {
        Number = Mathf.Clamp(Number + IncreaseNumber, 0, maximumValue);
    }

    public void SubNumber(int number)
    {
        Number = Mathf.Clamp(Number - number, 0, maximumValue);
    }

    public void SubNumber()
    {
        Number = Mathf.Clamp(Number - IncreaseNumber, 0, maximumValue);
    }
}
