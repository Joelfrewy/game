using UnityEngine;
using System.Collections;

public class Flag : MonoBehaviour {
	
	public GameObject spawner;
	public GameObject player;
	Move move;
	float dragend = 10f;
	// Use this for initialization
	void Start () {
		spawner = GameObject.Find ("Spawner");
		player = GameObject.Find ("Player");
		move = player.GetComponent < Move >();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x-spawner.transform.position.x > 30){
			Destroy(gameObject);
		}
		float zdist = transform.position.z - player.transform.position.z;
		float xdist = transform.position.x - player.transform.position.x;
		if (zdist > -1 && zdist < 1 && xdist > -dragend && xdist < 0 && move.velocity > -0.5) {
			move.velocity -= 0.003f*(xdist+dragend);
				}
	}
}
