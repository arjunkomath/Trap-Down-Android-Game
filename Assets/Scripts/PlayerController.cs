using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {

	Animator birdy;
	public float speed;
	public float driftSpeed;
	public Boundary boundary;

	bool dead =false, status;
	private Controller gameController;

	public AudioClip hit,score;
	int flaps=0;

	void Start ()
	{
		flaps = PlayerPrefs.GetInt ("Flaps",0);
		birdy = GetComponent<Animator> ();
		birdy.SetTrigger("DoFlap");
		if (birdy == null) {
			birdy = GetComponent<Animator> ();		
		}

		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Controller>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}


	// Update is called once per frame
	void FixedUpdate () {
		float moveLeft = Input.acceleration.x;

		Vector3 fly = new Vector3 (moveLeft * driftSpeed , 6.0f, 0.0f);

		rigidbody.position = new Vector3
			(
				Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
				Mathf.Clamp (rigidbody.position.y, boundary.yMin, boundary.yMax),
				0.0f
		);

		if (Input.touchCount>0 && !dead) {
			rigidbody.velocity = Vector3.Lerp(rigidbody.velocity , fly, speed * Time.deltaTime);
			birdy.SetTrigger("DoFlap");
			audio.Play();
			Flap ();
		}

	}

	float nextTime=0.0f;
	void Flap() {
		if (Time.time > nextTime) {
						Debug.Log (flaps);
						flaps+=1;
						nextTime = 0.5f + Time.time;
				}

		if (flaps > 100) {
			gameController.SaveAchieve ("CgkIo9C3xdQcEAIQCQ");		
		}
		if (flaps > 200) {
			gameController.SaveAchieve ("CgkIo9C3xdQcEAIQCg");		
		}
		if (flaps > 500) {
			gameController.SaveAchieve ("CgkIo9C3xdQcEAIQCw");		
		}
		if (flaps > 1000) {
			gameController.SaveAchieve ("CgkIo9C3xdQcEAIQDA");		
		}
		if (flaps > 5000) {
			gameController.SaveAchieve ("CgkIo9C3xdQcEAIQDQ");		
		}
	}

	void OnTriggerEnter(Collider bird) {
	status = gameController.gameStatus ();
		if (status == false) {
				if (bird.tag == "Score") {
				gameController.AddScore (1);
				Destroy(bird.gameObject);
				audio.PlayOneShot(score, 2.0f);
				} 
			else {
						audio.PlayOneShot(hit, 2.0f);
						birdy.SetTrigger ("Die");
						gameController.endGame ();
						dead = true;
						PlayerPrefs.SetInt ("Flaps",flaps);
						PlayerPrefs.Save ();
				}
			}
		}
}
