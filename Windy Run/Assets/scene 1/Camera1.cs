using UnityEngine;
using System.Collections;

public class Camera1 : MonoBehaviour {

	public GameObject spawner;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3((transform.position.x-spawner.transform.position.x-13) * -0.1f , 0.0f , 0.0f);
	}
}
