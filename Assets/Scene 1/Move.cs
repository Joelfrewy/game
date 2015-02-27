using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	bool alive;
	public float yvelocity;
	public float xvelocity;
	float rotatevelocity;
	public GameObject bar;
	GameObject child;
	public float zmouse;
	// Use this for initialization
	void Start () {
		alive = true;
		child = transform.GetChild(0).gameObject;
		zmouse = 0;
		yvelocity = 0.6f;
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
		yvelocity -= 0.006f;
		xvelocity = (zmouse - transform.position.z) * 0.3f;

		//update rotation and position
		float targetangle = -xvelocity * 90f;
		float currentangle = child.transform.eulerAngles.y;
		if (targetangle > 180) {targetangle -= 360;}
		if (currentangle > 180) {currentangle -= 360;}
		rotatevelocity = (targetangle - currentangle) * 0.3f;
		child.transform.Rotate (0 ,rotatevelocity, 0);
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