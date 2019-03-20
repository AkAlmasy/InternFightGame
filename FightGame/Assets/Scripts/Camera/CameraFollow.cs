using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {


    [SerializeField]
    private float XMax;
    [SerializeField]
    private float YMax;
    [SerializeField]
    private float XMin;
    [SerializeField]
    private float YMin;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private GameObject BottomLeftPoint;
    [SerializeField]
    private GameObject TopRightPoint;
    [SerializeField]
    private Camera MainCamera;

    void Start()
    {

        var vertExtent = MainCamera.orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;


        XMax = TopRightPoint.transform.position.x - horzExtent;
        XMin = BottomLeftPoint.transform.position.x + horzExtent;
        YMax = TopRightPoint.transform.position.y - vertExtent;
        YMin = BottomLeftPoint.transform.position.y + vertExtent;
    }
    /// <summary>
    /// Követi a karaktert adott határokon belül.
    /// </summary>
    void LateUpdate () {
        transform.position = new Vector3(Mathf.Clamp(target.position.x, XMin, XMax), Mathf.Clamp(target.position.y, YMin, YMax), transform.position.z);
	}

    
}
