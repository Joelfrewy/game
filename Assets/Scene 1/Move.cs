using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	bool alive;
	public float yvelocity;
	float xvelocity;
	float rotatevelocity;
	public GameObject bar;
	GameObject child;
	float zmouse;
	// Use this for initialization
	void Start () {
		alive = true;
		child = transform.GetChild(0).gameObject;
		zmouse = 0;
		yvelocity = 0.5f;
		xvelocity = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		CheckDead ();
		if (alive) {
			MovePlayer ();
		}
	}

	void MovePlayer(){
		if (Input.GetMouseButton(0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll (ray);
			foreach (RaycastHit hit in hits) {
				if (hit.transform.tag == "ground") {
					zmouse = hit.point.z;
				}
			}
		}
		yvelocity -= 0.005f;
		xvelocity = (zmouse - transform.position.z) * 0.3f;
		float angle = -xvelocity * 90f;
		float angle2 = child.transform.eulerAngles.y;
		if (angle > 180) {angle -= 360;}
		if (angle2 > 180) {angle2 -= 360;}
		rotatevelocity = (angle - angle2) * 0.3f;
		child.transform.Rotate (0, rotatevelocity, 0);
		transform.Translate(yvelocity, 0, xvelocity);
	}


	void CheckDead(){
		if ((bar.transform.position.x - transform.position.x) > 9) {
			alive = false;
			bar.GetComponent<Bar>().alive = false;
			GetComponent<Death>().Died (yvelocity,rotatevelocity);
		}
	}
}