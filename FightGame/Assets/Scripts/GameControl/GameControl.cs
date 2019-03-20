using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {
 
    [SerializeField]
    private SpawnDoor[] EnemySpawnerCounter;
    [SerializeField]
    private CounterManager WaveCounter;
    private float activeEnemySpawnerNumber = 0f;


    /// <summary>
    /// Aktív spawnerek irányítása.
    /// </summary>
    public float ActiveEnemySpawnerNumber
    {
        get
        {
            activeEnemySpawnerNumber = 0;
            for (int i = 0; i < EnemySpawnerCounter.Length; i++)
            {
                if (EnemySpawnerCounter[i].gameObject.activeInHierarchy == true)
                {
                    activeEnemySpawnerNumber += 1;
                }
            }
            return activeEnemySpawnerNumber;
        }

        set
        {
            activeEnemySpawnerNumber = value;
        }
    }

}
