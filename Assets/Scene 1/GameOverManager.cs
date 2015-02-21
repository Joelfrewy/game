using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	public bool fallen = false;
	public float delay = 5f;

	Animator anim;
	float restartTimer;

	void Awake(){
		anim = GetComponent<Animator> ();
		}
	
	// Update is called once per frame
	void Update () {
		if (fallen) {
			anim.SetTrigger("GameOver");
			restartTimer += Time.deltaTime;

			if(restartTimer >= delay){
				Application.LoadLevel(Application.loadedLevel);
			}
				}
	}
}
