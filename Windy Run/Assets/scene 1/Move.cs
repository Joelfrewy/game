using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	bool alive;
	public float velocity;
	// Use this for initialization
	void Start () {
		velocity = -0.5f;
		alive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (alive) {
			if(velocity < 0.2){
			velocity += 0.015f;
			}
			transform.Translate (velocity, 0, 0);
		}
	}
	public void Stop(){
		transform.Translate (10, 0, 0);
		alive = false;
	}
}