using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour {

    Vector3 birdInitPosition;
	public float flapSpeed = 5f;
	public float fallSpeed = 15f;
	public bool didFlap = false;
	public Vector3 jumpForce;
	Rigidbody rigidBody;
	GameManager mainMan;

    private AudioSource audioSource;
    public AudioClip flapSound;
    public AudioClip hitSound;
    public AudioClip scoreSound;

    private bool hitGround = false;

	void Start(){
        rigidBody = GetComponent<Rigidbody> ();
        audioSource = GetComponent<AudioSource>();
        birdInitPosition = this.transform.localPosition;
		mainMan = GameObject.Find ("GameManager").GetComponent<GameManager> ();
	}

	void Update () {
        if (mainMan.startedGame){
            rigidBody.useGravity = true;

            if (mainMan.stillAlive) {
                // Jump
                if (Input.GetKeyUp("space") || Input.GetMouseButtonUp(0)) {
                    rigidBody.velocity = Vector3.zero;
                    rigidBody.AddForce(jumpForce);
                    audioSource.PlayOneShot(flapSound);
                }

                if (rigidBody.velocity.y > 0) {
                    transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, 0);
                } else {
                    float angle = Mathf.Lerp(0, -90.0f, -rigidBody.velocity.y / 2);\
                    transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, angle);
                }

            }
        }else {
            rigidBody.useGravity = false;
        }
	}

    public void ResetBird() {
        print(birdInitPosition);
        this.gameObject.transform.localPosition = birdInitPosition;
        transform.rotation = Quaternion.identity;
        rigidBody.useGravity = false;
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX;
        rigidBody.velocity = Vector3.zero;
    }

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "floor") {
            audioSource.PlayOneShot(hitSound);
            mainMan.stillAlive = false;
        }
        rigidBody.constraints = RigidbodyConstraints.None;
	}

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "CounterTrigger"){
            print("Collided with points");
            audioSource.PlayOneShot(scoreSound);
            Destroy(other);
            mainMan.AddPoints(1);
        }
    }
}
