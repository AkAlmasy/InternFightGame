using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public float Speed;
    [Header(" ")]
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private EnemyMainScript EnemyMainScriptAsset;
    [SerializeField]
    private Animator animator;
    public Transform target;
    private float xscale;
    private Vector3 scale;
    public int EnemyIndex;

    private EnemyCollectionSO Collection => MapHolderSOAsset.CurrentMapSettings.EnemyCollections;

    [SerializeField]
    private MapHolderSO MapHolderSOAsset;
    [SerializeField]
    private WaveCounter WaveCounter;

    [Header("STANDARD ATTACK")]
    public float StartTimeBtwAttack;
    public float AttackRange;
    public int Damage;
    private float timeBtwAttack;


    /// <summary>
    /// Megkeresi a targetét (jelenleg a játékos)
    /// Feltölti az ellenfelet a megfelelő paraméterekkel az adott EnemyParamsSO-ból.
    /// </summary>
	void Start () {

        Speed = Collection.EnemyVariations[WaveCounter.WaveNumber - 1].EnemyArrayHolder[EnemyIndex].Enemy.MoveSpeed;
        StartTimeBtwAttack = Collection.EnemyVariations[WaveCounter.WaveNumber - 1].EnemyArrayHolder[EnemyIndex].Enemy.AttackSpeed;
        AttackRange = Collection.EnemyVariations[WaveCounter.WaveNumber - 1].EnemyArrayHolder[EnemyIndex].Enemy.AttackRange;
        Damage = Collection.EnemyVariations[WaveCounter.WaveNumber - 1].EnemyArrayHolder[EnemyIndex].Enemy.Damage;

        scale = transform.localScale;
	}


	/// <summary>
    /// Játékos felé mozgatja az Enemy-t és forgatja a sprite-ot + ha "AttackRange"-be kerül a játékos, akkor mozgatás helyett sebzi azt.
    /// </summary>
	void Update () {


        //távolság mérés / támadás
		if (Vector2.Distance(transform.position, target.position) > AttackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
            rb.velocity = rb.velocity.normalized * Speed;
        }
        else
        {
            if (timeBtwAttack <= 0)
            {
                EnemyMainScriptAsset.StandardAttack(Damage);
                animator.SetTrigger("AttackAnim");
                timeBtwAttack = StartTimeBtwAttack;
            }
        }
        if (timeBtwAttack != 0)
        {
            animator.SetTrigger("Running");
            timeBtwAttack -= Time.deltaTime;
        }

        //forgatás
        if (target.position.x > transform.position.x)
        {
            //face right
            xscale = 1;
        }
        else if (target.position.x < transform.position.x)
        {
            //face left
            xscale = -1;
            
        }
        transform.localScale = new Vector3(xscale * scale.x, scale.y, scale.z);
    }
}
