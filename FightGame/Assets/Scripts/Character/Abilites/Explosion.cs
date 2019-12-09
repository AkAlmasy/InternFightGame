using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {


    [SerializeField]
    private CircleCollider2D ExplosionCircle;
    [SerializeField]
    private PlayerAttack PlayerAttackAsset;
    [SerializeField]
    private float ExplosionDestroyTime = 2f;
    [SerializeField]
    private GameObject BloodSplash1;


    /// <summary>
    /// lekéri a playercharacter playerattack scriptjét.
    /// </summary>
    void Start()
    {
        PlayerAttackAsset = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        StartCoroutine(ExplosionDestroy());  
    }

    /// <summary>
    /// növeli a robbanás circle colliderét megadott sebességgel. 
    /// </summary>
    void Update () {
        ExplosionCircle.radius += PlayerAttackAsset.ExplosionExpansionSpeed * Time.deltaTime;
	}


    /// <summary>
    /// sebez minden "Enemy" taget amit elér.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss" || collision.gameObject.tag == "MegaBoss" || collision.gameObject.tag == "Sprinter")
        {
            collision.gameObject.GetComponent<EnemyMainScript>().TakeDamage(PlayerAttackAsset.ExplosionDamage);
            GameObject newBloodSplash = Instantiate(BloodSplash1, collision.gameObject.GetComponent<Transform>().position, transform.rotation);
            Destroy(newBloodSplash, 3);
        }
    }


    IEnumerator ExplosionDestroy()
    {
        yield return new WaitForSeconds(ExplosionDestroyTime);
        Destroy(gameObject);
    }
}
