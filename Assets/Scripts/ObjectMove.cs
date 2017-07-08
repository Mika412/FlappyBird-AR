using UnityEngine;
using System.Collections;

public class ObjectMove : MonoBehaviour {

	public float size;
	public Vector3 moveSpeed;
	private Vector3 destroyPos;
	private GameObject parent;
	private ObjectsMoveManager parentManager;
	public Vector3 resize;
	private bool grown = false;
	private bool calledSpawn = false;

    public GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = Object.FindObjectOfType<GameManager>();
        parent = this.transform.parent.gameObject;
		parentManager = parent.GetComponent<ObjectsMoveManager> ();
		destroyPos = parentManager.destroyPos;
	}

	void ResizeUpCheck(){
		if (this.transform.localScale.x >= 1.0f) {
			grown = true;
			this.transform.localScale = new Vector3 (1, 1, 1);
		} else {
			ResizeUp ();
		}
	}

	void ResizeDownCheck(){
		//Resize object down
		if (this.transform.localPosition.x <= destroyPos.x) {
			if (this.transform.localScale.x <= 0)
				Destroy (this.gameObject);
			else
				ResizeDown ();
		}
	}

	void ResizeUp(){
		this.transform.localScale += resize * Time.deltaTime;
	}

	void ResizeDown(){
		this.transform.localScale -= resize * Time.deltaTime;
	}

    // Update is called once per frame
    void FixedUpdate() {
        if (gameManager.startedGame && gameManager.stillAlive) {
            //Move object
            transform.localPosition += moveSpeed * Time.deltaTime;

        //Check if spawn another
        if (this.transform.localPosition.x < (size * parentManager.spawnFrequency * -1 ) && calledSpawn == false) {
            parentManager.SpawnCall();
            calledSpawn = true;
        }

        //Resize check
        if (grown == false) {
            ResizeUpCheck();
        } else {
            ResizeDownCheck();
        }
        }
	}
}
