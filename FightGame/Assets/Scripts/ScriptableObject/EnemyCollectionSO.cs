using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Adott pályára az Ellenfél hullámokat tartalmazó SO
/// </summary>
[CreateAssetMenu(fileName = "EnemyCollection", menuName = "Create EnemyCollectionSO")]
public class  EnemyCollectionSO : ScriptableObject {


    [System.Serializable]
    public class EnemyArray
    {
        public int NumberOfEnemiesInWave;
        public EnemyParamsSO Enemy;
    }
    [System.Serializable]
    public class EnemyHolder
    {
        public string WaveName;
        public EnemyArray[] EnemyArrayHolder;
    }

    public EnemyHolder[] EnemyVariations;


    /// <summary>
    /// adott wave össz elküldendő ellenfelének összeszámolása
    /// </summary>
    /// <param name="waveindex"></param>
    /// <returns></returns>
    public int GetAllEnemyNumberInWave(int waveindex)
    {
        int EnemyNumberSum = 0;
        for (int i = 0; i < EnemyVariations[waveindex].EnemyArrayHolder.Length; i++)
        {
            EnemyNumberSum += EnemyVariations[waveindex].EnemyArrayHolder[i].NumberOfEnemiesInWave;
        }
        return EnemyNumberSum;
    }

}



