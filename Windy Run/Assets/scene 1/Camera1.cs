using UnityEngine;
using System.Collections;

public class Camera1 : MonoBehaviour {

	public GameObject bar;
	float velocity = 0;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		velocity = (bar.transform.position.x - transform.position.x)  * 0.1f;
		transform.Translate (velocity, 0, 0);
	}
}
