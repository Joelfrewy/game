using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour {

	GameObject player;
	Move move;
	float xdist;
	float zdist;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("PlayerAnchor");
		move = player.GetComponent<Move> ();
	}

	// Update is called once per frame
	void Update () {
		xdist = player.transform.position.x - transform.position.x;
		zdist = player.transform.position.z - transform.position.z;
		//if the player is within the bounds of the object
		if (Mathf.Abs (xdist) < 1 &&
			Mathf.Abs (zdist) < 1) {
				Collision();
		}
	}

	void Collision(){
		if(Mathf.Abs (xdist) > Mathf.Abs (zdist)){
			if(xdist > 0 && move.yvelocity < 0){
				move.yvelocity = 0.35f;
			}
			else{
				move.yvelocity = -0.1f;
			}
		}
		else {
			if(zdist > 0){
				player.transform.Translate(0,0,move.xvelocity);
			}
			else{
				player.transform.Translate(0,0,-move.xvelocity);
			}
		}

	}

}
