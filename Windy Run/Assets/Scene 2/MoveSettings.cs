using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveSettings : MonoBehaviour {

	public GameObject spin;
	public GameObject rot;
	public float sensitivity = 0.5f;
	public Button addbutton;
	public Button rembutton;
	public GameObject builder;
	public bool click;
	public float clickdist = 2f;
	public bool add = true;
	public Vector2 start;
	public Vector2 current;

	bool swiping;
	float startspin;
	float startrot;

	bool zooming;
	float touchdist;
	// Use this for initialization
	void Start () {
		swiping = false;
		zooming = false;
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
		if (swiping) {
	
			spin.transform.Rotate(0,-(current.x - newpos.x) * sensitivity, 0);

			float temprot = rot.transform.eulerAngles.x;
			if(temprot > 180){temprot -= 360;}
			temprot = Mathf.Max (Mathf.Min (80,temprot),-80);
			if(temprot < 80 && current.y > newpos.y || temprot > -80 && current.y < newpos.y){
				rot.transform.Rotate((current.y - newpos.y) * sensitivity, 0,0);
			}

			if (current.y - start.y > clickdist || current.y - start.y < -clickdist || 
			current.x - start.x > clickdist || current.x - start.x < -clickdist) {
				click = false;
				}
			} else {
			swiping = true;
			click = true;
			start = Input.GetTouch (0).position;
			}
			current = newpos;
		}

	public void SwipeEnd(){
		swiping = false;
		if (click) {
			if(add){
				builder.GetComponent<Builder>().AddBlock();
			}else{
				builder.GetComponent<Builder>().RemoveBlock();
			}
		}
	}
}
