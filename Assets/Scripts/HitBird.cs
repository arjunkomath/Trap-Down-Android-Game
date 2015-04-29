using UnityEngine;
using System.Collections;

public class HitBird : MonoBehaviour {

	public GameObject left, right,collide;

	private Controller gameController;

	bool status;

	float speed;

	void Start ()
	{
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
	
	void Update () {
		status = gameController.gameStatus ();
			if (!status) {
			speed = gameController.getSpeed();
			if(left.gameObject != null && right.gameObject != null && collide.gameObject != null) {
					left.rigidbody.velocity = new Vector3 (0.0f, 1.0f * speed, 0.0f);
					right.rigidbody.velocity = new Vector3 (0.0f, 1.0f * speed, 0.0f);
					collide.rigidbody.velocity = new Vector3 (0.0f, 1.0f * speed, 0.0f);	
				}
			}
		}
}
