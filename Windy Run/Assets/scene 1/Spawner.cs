using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	
	public GameObject flag;
	public GameObject player;
	public GameObject quad;
	public GameObject quad2;
	public GameObject board;
	public GameObject Canvas;
	GameOverManager gameoverscript;
	Move move;
	int blockcount;
	int quadcount;
	// Use this for initialization
	void Start () {
		blockcount = 2;
		quadcount = 12;
		move = player.GetComponent<Move> ();
		gameoverscript = Canvas.GetComponent<GameOverManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		float spawnx = transform.position.x;
		if (player.transform.position.x - spawnx < 10) {
			transform.position = new Vector3 (player.transform.position.x - 10, 0.0f, 0.0f);
		}
		if (player.transform.position.x - spawnx > 20) {
			gameoverscript.fallen = true;
			transform.position = new Vector3 (player.transform.position.x - 10, 0.0f, 0.0f);
		}
		if (- spawnx / 5 > blockcount) {
			SpawnFlag ();
			blockcount++;
		}
		if (- spawnx > quadcount) {
			SpawnQuad ();
			quadcount++;
		}
	}
	
	void SpawnFlag (){
		GameObject newflag = Instantiate (flag, new Vector3 (-(blockcount*5), 0, Random.Range(-2,3)*1.3f), Quaternion.identity) as GameObject;
		newflag.transform.parent = board.transform;
	}

	void SpawnQuad (){
		GameObject current;
		if (quadcount % 2 == 0) {
			current = quad;
		} else {
			current = quad2;
		}
			GameObject newquad = Instantiate (current, new Vector3 (-quadcount, -0.5f, 0), Quaternion.Euler (90, 0, 0)) as GameObject;
			newquad.transform.parent = board.transform;
	}
}
