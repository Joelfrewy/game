using UnityEngine;
using System.Collections;

public class GroundAdd : MonoBehaviour {

	GameObject frontspawn;
	GameObject backspawn;
	public GameObject ground1;
	public GameObject ground2;
	// Use this for initialization
	void Start () {
		frontspawn = GameObject.Find("Front Spawn");
		backspawn = GameObject.Find("Back Spawn");
		GameObject g1 = Instantiate (ground1, transform.position, Quaternion.Euler (90, 0, 0)) as GameObject;
		GameObject g2 = Instantiate (ground2, transform.position + new Vector3(1,0,0), Quaternion.Euler (90, 0, 0)) as GameObject;
		g1.transform.parent = transform;
		g2.transform.parent = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x > frontspawn.transform.position.x || transform.position.x < backspawn.transform.position.x){
		Destroy (gameObject);
		}
	}
}
