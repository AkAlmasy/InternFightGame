using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMainScript : MonoBehaviour {

    public int health;
    [SerializeField]
    private int manaWorth;
    [SerializeField]
    private int currencyWorth;
    [SerializeField]
    private int healthGain;
    public int DeathBounceTime = 2;
    private bool IsDead = false;
    [SerializeField]
    private float thrust = 10.0f;
    public int EnemyIndex = 0;


    [Header(" ")]
    [SerializeField]
    private CounterManager EnemyNumber;
    [SerializeField]
    private CounterManager ManaCounter;
    [SerializeField]
    private CounterManager CurrencyCounter;
    [SerializeField]
    private CounterManager PlayerHealth;
    [SerializeField]
    private WaveCounter WaveCounter;
    [SerializeField]
    private EnemyMove EnemyMoveAsset;
    [SerializeField]
    private Rigidbody2D rb2DAsset;

    [SerializeField]
    private MapHolderSO MapHolderSOAsset;

    private EnemyCollectionSO Collection => MapHolderSOAsset.CurrentMapSettings.EnemyCollections;

    /// <summary>
    /// hullámonkénti hp növelés + érték lekérés az adott EnemyParamSO-tól.
    /// </summary>
    void Start()
    {
        health = Collection.EnemyVariations[WaveCounter.WaveNumber - 1].EnemyArrayHolder[EnemyIndex].Enemy.Health;
        manaWorth = Collection.EnemyVariations[WaveCounter.WaveNumber - 1].EnemyArrayHolder[EnemyIndex].Enemy.ManaWorth;
        currencyWorth = Collection.EnemyVariations[WaveCounter.WaveNumber - 1].EnemyArrayHolder[EnemyIndex].Enemy.CurrencyWorth;
        healthGain = Collection.EnemyVariations[WaveCounter.WaveNumber - 1].EnemyArrayHolder[EnemyIndex].Enemy.HealthGainPerLevel;
        thrust = Collection.EnemyVariations[WaveCounter.WaveNumber - 1].EnemyArrayHolder[EnemyIndex].Enemy.DeathThrust;
        DeathBounceTime = Collection.EnemyVariations[WaveCounter.WaveNumber - 1].EnemyArrayHolder[EnemyIndex].Enemy.GameObjectDestroyTime;

        health += healthGain * WaveCounter.WaveNumber;
    }


    /// <summary>
    /// regisztrálja a beérkezett sebzéseket, halál esetén "ragdoll"-t csinál az ellenfélből és félig randomizált irányba fellöki. 
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0 && IsDead == false)
        {
            EnemyNumber.SubNumber();
         
            ManaCounter.AddNumber(manaWorth);

            CurrencyCounter.AddNumber(currencyWorth);
  
            Vector2 flightDirection = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0.75f, 1.0f));
            GetComponent<EnemyMove>().enabled = false;
            rb2DAsset.constraints = RigidbodyConstraints2D.None;
            GetComponent<CircleCollider2D>().enabled = false;
            rb2DAsset.AddForce(flightDirection * thrust);
            rb2DAsset.gravityScale = 5;
            IsDead = true;
            StartCoroutine(DeathBounce());
        }
    }
    /// <summary>
    /// Enemy Halál esetén adott idő után törli az Enemy GameObjektet.
    /// </summary>
    /// <returns></returns>
    IEnumerator DeathBounce()
    {
        yield return new WaitForSeconds(DeathBounceTime);
        Destroy(gameObject);
    }


    public void StandardAttack(int damage)
    {
        PlayerHealth.SubNumber(damage);
    }
}
