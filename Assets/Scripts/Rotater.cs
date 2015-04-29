using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {

	public float angle = 90.0f;
	public float speed = 3f;
	
	Quaternion qStart, qEnd;
	
	void Start () {
		qStart = Quaternion.AngleAxis ( angle, Vector3.forward);
		qEnd   = Quaternion.AngleAxis (-angle, Vector3.forward);
	}
	
	void Update () {
		transform.rotation = Quaternion.Lerp (qStart, qEnd, (Mathf.Sin(Time.time * speed) + 1.0f) / 2.0f);
	}
}
