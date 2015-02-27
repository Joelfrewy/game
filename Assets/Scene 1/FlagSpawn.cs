using UnityEngine;
using System.Collections;

public class FlagSpawn : MonoBehaviour {

	public GameObject board;
	public GameObject flag;
	int flagcount;
	// Use this for initialization
	void Start () {
		flagcount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x - 13 > flagcount * 6) {
			GameObject g = Instantiate(flag,new Vector3(transform.position.x,
			                                            transform.position.y + 0.5f,
			                                            Random.Range (-2f,3f)),Quaternion.Euler(0,0,0)) as GameObject;
			g.transform.parent = board.transform;
			flagcount++;
		}
	}
}
