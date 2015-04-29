using UnityEngine;
using System.Collections;

public class KillEverything : MonoBehaviour {

	void OnTriggerEnter ( Collider thing ) {
		Destroy (thing.gameObject);
	}
}
