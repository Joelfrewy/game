using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveSettings : MonoBehaviour {

	public GameObject spin;
	public GameObject rot;
	public GameObject builder;

	public float sensitivity;
	public Button addbutton;
	public Button rembutton;


	public bool click;
	public float clickdist = 2f;

	public bool add = true;
	public Vector2 current;

	bool swiping;
	bool zooming;
	float touchdist;
	// Use this for initialization
	void Start () {
		click = true;
		swiping = false;
		zooming = false;
		sensitivity = Camera.main.orthographicSize * 0.02f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount >= 2) {
			swiping = false;
			Zoom ();
		} else if (Input.touchCount == 1) {
			if (!zooming)
				Swipe ();

		} else if (swiping && !zooming) {
			SwipeEnd ();
		} else {
			swiping = false;
			zooming = false;
		}

	}

	public void Zoom(){
		float newdist = Vector2.Distance (Input.GetTouch (0).position, Input.GetTouch (1).position);
		if (zooming) {
			Camera.main.orthographicSize += (touchdist - newdist) * 0.1f;
			Camera.main.orthographicSize = Mathf.Max (Mathf.Min (30,Camera.main.orthographicSize),2);
			sensitivity = Camera.main.orthographicSize * 0.02f;
		} else {
			zooming = true;
		}
		touchdist = newdist;
	}

	public void Add(){
		add = true;
		//highlight add button
		var colors = addbutton.colors;
		colors.normalColor = Color.green; 
		colors.pressedColor = Color.green;
		addbutton.colors = colors;
		//unhighlight remove button
		colors = rembutton.colors;
		colors.normalColor = Color.white; 
		colors.pressedColor = Color.white;
		rembutton.colors = colors;
	}

	public void Remove(){
		add = false;
		//unhighlight add button
		var colors = addbutton.colors;
		colors.normalColor = Color.white; 
		colors.pressedColor = Color.white;
		addbutton.colors = colors;
		//highlight remove button
		colors = rembutton.colors;
		colors.normalColor = Color.green; 
		colors.pressedColor = Color.green;
		rembutton.colors = colors;
	}

	public void Swipe(){
		Vector2 newpos = Input.GetTouch (0).position;
		swiping = true;
		if (Input.GetTouch(0).phase == TouchPhase.Moved) {
	
			spin.transform.Rotate(0,-(current.x - newpos.x) * sensitivity, 0);

			//set rotation angle limits
			float temprot = rot.transform.eulerAngles.x;
			if(temprot > 180){temprot -= 360;}
			temprot = Mathf.Max (Mathf.Min (80,temprot),-80);
			if(temprot < 80 && current.y > newpos.y || temprot > -80 && current.y < newpos.y){
				rot.transform.Rotate((current.y - newpos.y) * sensitivity, 0,0);
			}
		click = false;
		}
		current = newpos;
	}

	public void SwipeEnd(){
		if (click) {
			if(add){
				builder.GetComponent<Builder>().AddBlock();
			}else{
				builder.GetComponent<Builder>().RemoveBlock();
			}
		}
		swiping = false;
		click = true;
	}
}
