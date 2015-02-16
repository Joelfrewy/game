using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveSettings : MonoBehaviour {

	public GameObject spin;
	public GameObject rot;
	public Button addbutton;
	public Button rembutton;
	public GameObject builder;
	public bool swiping;
	public bool click;
	public float clickdist = 2f;
	public bool add = true;
	public Vector3 start;
	public Vector3 current;
	float startspin;
	float startrot;
	// Use this for initialization
	void Start () {
		swiping = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (swiping) {

			current = Input.mousePosition;


			spin.transform.rotation = Quaternion.Euler(spin.transform.eulerAngles.x, 
			                                           current.x - start.x + startspin, 
			                                           spin.transform.eulerAngles.z);

			rot.transform.rotation = Quaternion.Euler(Mathf.Max(Mathf.Min(start.y - current.y + startrot, 90),-90), 
			                                          rot.transform.eulerAngles.y, 
			                                          rot.transform.eulerAngles.z);
		
			if(current.y - start.y > clickdist || current.y - start.y < -clickdist || 
			   current.x - start.x > clickdist || current.x - start.x < -clickdist){
				click = false;
			}
		}

	}

	public void SwipeStart(){
		start = Input.mousePosition;
		startspin = spin.transform.eulerAngles.y;
		startrot = rot.transform.eulerAngles.x;
		if (startrot > 180) {startrot = startrot-360;}
		swiping = true;
		click = true;
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

	public void PrintNow(){
		Camera.main.orthographicSize -= 0.1f;
	}
}
