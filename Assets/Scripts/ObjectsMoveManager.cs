using UnityEngine;
using System.Collections;

public class ObjectsMoveManager : MonoBehaviour {

	public GameObject spawnPrefab;

	public Vector3 spawnPos;
	public Vector3 destroyPos;

    public int spawnFrequency;

	void Start () {
        SpawnCall();
	}

	public void SpawnCall(){
        print("Called");
		GameObject temp = Instantiate(spawnPrefab);
		temp.transform.parent = this.transform;
		temp.transform.localPosition = spawnPos;
	}
}
