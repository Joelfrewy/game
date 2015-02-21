using UnityEngine;
using System.Collections;

public class Bar : MonoBehaviour {

	public GameObject player;
	public bool alive;
	public float velocity;

	// Use this for initialization
	void Start () {
		alive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (alive) {
			transform.position = new Vector3 (Mathf.Max (transform.position.x, player.transform.position.x), 0.5f, 0);
		} 
	}
}
