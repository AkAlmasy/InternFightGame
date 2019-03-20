using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableShop : MonoBehaviour {
    [SerializeField]
    private Canvas ShopCanvas;


    /// <summary>
    /// Ha benne állunk a shopban, megjelenik a shop canvas.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ShopCanvas.gameObject.SetActive(true); 
    }

    /// <summary>
    /// ha elhagyjuk a shopot, eltűnik a shop canvas.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit2D(Collider2D other)
    {
        ShopCanvas.gameObject.SetActive(false);
    }

}
