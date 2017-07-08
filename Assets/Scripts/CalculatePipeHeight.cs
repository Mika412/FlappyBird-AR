using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatePipeHeight : MonoBehaviour {

    public GameObject top;
    public GameObject bottom;

    private int maxHeight = 5;

	void Start () {
        int height = Random.Range(1, 5);
        print(height);
        top.transform.localScale = new Vector3(top.transform.localScale.x,height ,top.transform.localScale.z);
        bottom.transform.localScale = new Vector3(top.transform.localScale.x, maxHeight - height,top.transform.localScale.z);
    }
}
