using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    [Header("STANDARD ATTACK")]
    public float StartTimeBtwAttack;
    public float AttackRange;
    public int BaseAttackDamage;
    private float timeBtwAttack;
 
    [Header("FIRE BALL")]
    public float StartTimeBtwFireBalls;
    public int FireBallDamage;
    public int FireBallManaCost;
    public float SpellCastTime = 1f;
    private float timeBtwFireBalls;

    [Header("HEAL")]
    public float StartTimeBtwHeals;
    public int HealManaCost;
    public int HealAmount;
    public float HealCastTime = 1f;
    private float timeBtwHeals;

    [Header("EXPLOSION")]
    public float StartTimeBtwExplosions;
    public float ExplosionExpansionSpeed = 0.5f;
    public int ExplosionDamage = 50;
    public int ExplosionManaCost;
    private float timeBtwExplosions;
    public float ExplosionCastTime = 1f;




    [Header(" ")]
    [SerializeField]
    private Animator AttackAnim;
    [SerializeField]
    private Transform AttackPosition;
    [SerializeField]
    private LayerMask WhatIsEnemies;
    [SerializeField]
    private GameObject fireballPrefab;
    [SerializeField]
    private Transform fireballposition;
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private CounterManager ManaCounter;
    [SerializeField]
    private Heal HealAsset;
    [SerializeField]
    private PlayerMovement PlayerMovementAsset;
    [SerializeField]
    private GameObject BloodSplash1;
    [SerializeField]
    private GameObject BloodSplashBoss;
    [SerializeField]
    private GameObject BloodSplashMegaBoss;

    /// <summary>
    /// Figyeli a lenyomott billentyűt,meghívja a megfelelő támadás methodját és elindítja a hozzá tartozó cooldownt. 
    /// </summary>
    // Update is called once per frame
    void Update () {
        

		if (Input.GetMouseButtonDown(0))
        {
            if (timeBtwAttack <= 0)
            {
                BaseAttack();
            }
        } 
        else if (Input.GetMouseButtonDown(1))
        {
            if (timeBtwFireBalls <= 0 && ManaCounter.Number >= FireBallManaCost)
            {
                StartCoroutine(FireBallCast());
            }
        }
        else if (Input.GetKeyDown("f"))
        {
            if (timeBtwHeals <= 0 && ManaCounter.Number >= HealManaCost)
            {
                StartCoroutine(HealCast());
            }
        }

        else if (Input.GetKeyDown("e"))
        {
            if (timeBtwExplosions <= 0 && ManaCounter.Number >= ExplosionManaCost)
            {
                StartCoroutine(ExplosionCast());
            }
        }


        if (StartTimeBtwHeals != 0)
        {
            timeBtwHeals -= Time.deltaTime;
        }

        if (StartTimeBtwFireBalls != 0)
        {
            timeBtwFireBalls -= Time.deltaTime;
        }

        if (timeBtwAttack != 0)
        {
            timeBtwAttack -= Time.deltaTime;
        }
        if (StartTimeBtwExplosions != 0)
        {
            timeBtwExplosions -= Time.deltaTime;
        }

    }




    /// <summary>
    /// Alap támadás méretét jelzi,
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPosition.position, AttackRange);
    }
    /// <summary>
    /// Alap Támadás, OverlapCricleAll csinál egy kört, össezszed mindent aminek Enemy Layer-e van és sebzi azokat.
    /// </summary>
    public void BaseAttack()
    {
        AttackAnim.SetTrigger("attack");
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(AttackPosition.position, AttackRange, WhatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyMainScript>().TakeDamage(BaseAttackDamage);
            if (enemiesToDamage[i].tag == "Enemy" || enemiesToDamage[i].tag == "Sprinter")
            {
                GameObject newBloodSplash = Instantiate(BloodSplash1, enemiesToDamage[i].transform.position, transform.rotation);
                Destroy(newBloodSplash, 3);
            }
            if (enemiesToDamage[i].tag == "Boss")
            {
                GameObject newBloodSplash = Instantiate(BloodSplashBoss, enemiesToDamage[i].transform.position, transform.rotation);
                Destroy(newBloodSplash, 3);
            }
            if (enemiesToDamage[i].tag == "MegaBoss")
            {
                GameObject newBloodSplash = Instantiate(BloodSplashMegaBoss, enemiesToDamage[i].transform.position, transform.rotation);
                Destroy(newBloodSplash, 3);
            }
        }
        timeBtwAttack = StartTimeBtwAttack;
    }


    /// <summary>
    /// FireBallt kezeli, animáció + instantiate prefab + átadja a FireBallnak a játékoshoz képest a helyzetét.
    /// </summary>
    /// <returns></returns>
    IEnumerator FireBallCast()
    {
        ManaCounter.SubNumber(FireBallManaCost);
        AttackAnim.SetTrigger("SpellCast");
        timeBtwFireBalls = StartTimeBtwFireBalls;
        yield return new WaitForSeconds(SpellCastTime);
        var go = Instantiate(fireballPrefab, fireballposition.position, transform.rotation);
        go.GetComponent<FireBallScript>().Player = gameObject;
    }

    /// <summary>
    /// Robbanás képességet kezeli, megállítja a játékos mozgását, elindítja az animációt, levonja a Mana költséget, elindítja a CoolDownt, instantiate-eli a robbanás prefabot.
    /// </summary>
    /// <returns></returns>
    IEnumerator ExplosionCast()
    {
        PlayerMovementAsset.CanMove = false;
        ManaCounter.SubNumber(ExplosionManaCost);
        AttackAnim.SetTrigger("Explosion");
        timeBtwExplosions = StartTimeBtwExplosions;
        yield return new WaitForSeconds(ExplosionCastTime);
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        PlayerMovementAsset.CanMove = true;
        
    }

    /// <summary>
    /// Gyógyítás képességet kezeli, animáció + CoolDown + hívja a heal metódust.
    /// </summary>
    /// <returns></returns>
    IEnumerator HealCast()
    {
        AttackAnim.SetTrigger("Healing");
        timeBtwHeals = StartTimeBtwHeals;
        yield return new WaitForSeconds(HealCastTime);
        HealAsset.Healing(HealAmount, HealManaCost);
    }
}
