using UnityEngine;
using System.Collections;

public class FrontSpawn : MonoBehaviour {

	public GameObject board;
	public GameObject player;
	public GameObject ground;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x - player.transform.position.x < 13) {
			GameObject g = Instantiate(ground,transform.position,Quaternion.Euler(0,0,0)) as GameObject;
			g.transform.parent = board.transform;
			transform.Translate(2,0,0);
		}
		if (transform.position.x - player.transform.position.x > 29) {
			transform.Translate(-2,0,0);
		}
	}
}
