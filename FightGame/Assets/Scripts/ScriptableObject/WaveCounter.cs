using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CounterManager", menuName = "Create WaveCounter")]
public class WaveCounter : CounterManager {

    /// <summary>
    /// Ellenfél hullámok számolására.
    /// </summary>
    [SerializeField]
    private int waveNumber = 1;


    public int WaveNumber
    {
        get
        {
            return waveNumber;
        }

        set
        {
            waveNumber = value;
        }
    }

}
