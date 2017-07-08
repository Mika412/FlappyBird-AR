using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public bool startedGame = false;
	public bool stillAlive = true;
    public int points = 0;

    public GameObject pressText;
    public TextMesh scoreText;
    public BirdMovement birdMovementScript;
    public GameObject[] movableObjectParents;

    private void Update(){
        if (!startedGame && Input.GetMouseButtonUp(0)){
            pressText.SetActive(false);
            startedGame = true;
        }
        if (startedGame){
            scoreText.text = points+"";
        }

        if (Input.GetKeyUp("space") || Input.GetMouseButtonUp(0) && !stillAlive && startedGame) {
            ResetGame();
        }
    }

    public void AddPoints(int points) {
        this.points += points;
    }

    public void ResetGame(){
        scoreText.text = "";
        startedGame = false;
        stillAlive = true;
        pressText.SetActive(true);
        foreach (GameObject gb in movableObjectParents) {
            foreach (Transform child in gb.transform) {
                Destroy(child.gameObject);
            }
            gb.GetComponent<ObjectsMoveManager>().SpawnCall();
        }
        birdMovementScript.ResetBird();
    }
}
