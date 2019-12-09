using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour {

    public float Speed = 5f;
    private bool FromRight = true;
    [SerializeField]
    private readonly float FireBallDestroyTime = 2f;
    public Transform player => Player.transform;
    [SerializeField]
    private PlayerAttack PlayerAttackAsset;
    [HideInInspector]
    public GameObject Player;
    [SerializeField]
    private GameObject FireBallHitEffect;
  




    /// <summary>
    /// Megkeresi a Játékost, eldönti, hogy melyik irányba néz éppen.
    /// </summary>
    // Use this for initialization
    void Start () {
        PlayerAttackAsset = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>();
        StartCoroutine(FireBallDestroy());
        if (player.position.x > transform.position.x)
        {
            //face left
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            FromRight = false;

        }
        else if (player.position.x < transform.position.x)
        {
            //face right
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            FromRight = true;
        }
    }


	/// <summary>
    /// mozgatja a GameObjectet
    /// </summary>
	// Update is called once per frame
	void Update () {
        if (FromRight)
        {
            transform.Translate(Speed * Time.deltaTime, 0, 0);
        }else
        {
            transform.Translate(-Speed * Time.deltaTime, 0, 0);
        }
    }


    /// <summary>
    /// érzékeli ha a fireball ellenfélhez vagy földbe rejtett blokkhoz ér.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss" || collision.gameObject.tag == "MegaBoss")
        {
            collision.gameObject.GetComponent<EnemyMainScript>().TakeDamage(PlayerAttackAsset.FireBallDamage);
            GameObject newFireballEffect = Instantiate(FireBallHitEffect,collision.gameObject.GetComponent<Transform>().transform.position, transform.rotation);
            Destroy(newFireballEffect, FireBallDestroyTime);
        }else if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// 2 másodperc után törli a fireballt.
    /// </summary>
    /// <returns></returns>
    IEnumerator FireBallDestroy()
    {
        yield return new WaitForSeconds(FireBallDestroyTime);
        
        Destroy(gameObject);
    }
}
