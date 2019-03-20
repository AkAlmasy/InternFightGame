using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour {

    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private int RestTime;
    [SerializeField]
    private int EnemyCount = 0;
    private int EnemyTypeCounter = 0;
    private int EnemyIndex = 0;


    [Header(" ")]
    [SerializeField]
    private CounterManager EnemyCounter;
    [SerializeField]
    private WaveCounter WaveCounter;
    [SerializeField]
    private GameControl GameControlAsset;
    [SerializeField]
    private GameObject[] Spawnpoints;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private MapHolderSO MapHolderSOAsset;

    private EnemyCollectionSO Collection => MapHolderSOAsset.CurrentMapSettings.EnemyCollections;



    void Start()
    {
        StartCoroutine(Spawn());
    }

    /// <summary>
    /// hullám max ellenfél számáig vagy adott wave össze ellenfél számáig, majd hullámok közti szünet.
    /// </summary>
    void SpawnPoint()
    {
        if (EnemyCount >= WaveCounter.MaximumValue || EnemyCount >= Collection.GetAllEnemyNumberInWave(WaveCounter.WaveNumber-1))
        {
            Debug.Log(GameControlAsset.ActiveEnemySpawnerNumber);
            StartCoroutine(NextWaveTimer());
            EnemyTypeCounter = 0;
        }
        else
        {
            
            if (EnemyTypeCounter >= Collection.EnemyVariations[WaveCounter.WaveNumber - 1].EnemyArrayHolder[EnemyIndex].NumberOfEnemiesInWave)
            {
                EnemyIndex++;
                EnemyTypeCounter = 0;
                StartCoroutine(MidWaveTimer());
            }
            else
            {
                StartCoroutine(Spawn());
            }
        }
    }
    /// <summary>
    /// Enemy Prefabot instantiate-el adott időközönként, adott helyekre (amennyiben azok "nyitva" vannak).
    /// </summary>
    /// <returns></returns>
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnTime);
        for (int i = 0; i < Spawnpoints.Length; i++)
        {
            if (Spawnpoints[i].gameObject.activeInHierarchy == true)
            {
                if (EnemyTypeCounter >= Collection.EnemyVariations[WaveCounter.WaveNumber - 1].EnemyArrayHolder[EnemyIndex].NumberOfEnemiesInWave)
                {
                    break;
                }
                GameObject go = Instantiate(Collection.EnemyVariations[WaveCounter.WaveNumber - 1].EnemyArrayHolder[EnemyIndex].Enemy.Prefab, Spawnpoints[i].transform.position, transform.rotation);
                go.GetComponent<EnemyMove>().target = Player.transform;
                go.GetComponent<EnemyMainScript>().EnemyIndex = EnemyIndex;
                go.GetComponent<EnemyMove>().EnemyIndex = EnemyIndex;
                EnemyCounter.AddNumber();
                WaveCounter.AddNumber();
                EnemyCount++;
                EnemyTypeCounter++;
            }
        }
        SpawnPoint();
    }


    /// <summary>
    /// Ha a hullám elérte a maximum ellenfél számát, reseteli azt, vár adott időt.
    /// </summary>
    /// <returns></returns>
    IEnumerator NextWaveTimer()
    {
        yield return new WaitForSeconds(RestTime);
        WaveCounter.Reset();
        EnemyCount = 0;
        WaveCounter.WaveNumber += 1;
        EnemyIndex = 0;
        SpawnPoint();
    }


    /// <summary>
    /// Waven belül 2 különböző típusú ellenfél közti várakozás ideje.
    /// </summary>
    /// <returns></returns>
    IEnumerator MidWaveTimer()
    {
        yield return new WaitForSeconds(Collection.EnemyVariations[WaveCounter.WaveNumber-1].EnemyArrayHolder[EnemyIndex].Enemy.WaitBeforeThisType);
        StartCoroutine(Spawn());
    }
}
