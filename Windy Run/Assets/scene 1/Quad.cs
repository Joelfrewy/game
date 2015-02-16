using UnityEngine;
using System.Collections;

public class Quad : MonoBehaviour {

	public GameObject spawner;
	// Use this for initialization
	void Start () {
		spawner = GameObject.Find ("Spawner");
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x-spawner.transform.position.x > 30){
			Destroy(gameObject);
		}
	}
}
