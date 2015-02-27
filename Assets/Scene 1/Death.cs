using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {

	public bool alive;
	public bool end;
	public bool stopped;
	float yvelocity;
	//float xrotatevelocity;
	float yrotatevelocity;
	//float zrotatevelocity;
	float deathtime;
	public GameObject bar;
	GameObject child;
	// Use this for initialization
	void Start () {
		alive = true;
		end = false;
		stopped = false;
		child = transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (!alive && !stopped) {
			DeathLoop ();		
		}
		if (end) {
			EndLoop ();
		}
	}

	public void Died(float yvel, float yrotvel){
		alive = false;
		deathtime = Time.time;
		yvelocity = yvel;
		yrotatevelocity = yrotvel;
		//xrotatevelocity = 0;
		//zrotatevelocity = 0;
	}

	void DeathLoop(){
		if (!end) {
			bar.transform.position = transform.position + new Vector3(-3,0,0);
		}
		if (Time.time - deathtime > 0.5f && !end) {
			end = true;
			bar.GetComponent<Bar>().velocity = yvelocity;
		}
		yrotatevelocity += Random.Range (-0.6f,0.6f);
		//xrotatevelocity += Random.Range (-0.6f,0.6f);
		//zrotatevelocity += Random.Range (-0.6f,0.6f);
		//child.transform.Rotate (xrotatevelocity, yrotatevelocity, zrotatevelocity);
		child.transform.Rotate (0, yrotatevelocity, 0);
		yvelocity -= 0.005f;
		transform.Translate(yvelocity, 0, 0);
	}

	void EndLoop(){
		bar.GetComponent<Bar>().velocity = Mathf.Min (0,bar.GetComponent<Bar>().velocity += 0.01f);
		bar.transform.Translate(bar.GetComponent<Bar>().velocity,0,0);
		if(bar.transform.position.x - transform.position.x > 12){
			stopped = true;
			Application.LoadLevel ("Windy Run");
		}
	}
}
