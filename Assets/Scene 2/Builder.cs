using UnityEngine;
using System.Collections;

public class Builder : MonoBehaviour {
	
	public GameObject quad;
	public GameObject body;
	public Color color;
	public int size = 4;
	Color[,,] array;
	Color[,,] array2;
	// Use this for initialization
	void Start () {
		array = new Color[16,16,16];
		array2 = new Color[8,8,8];
		DrawArray ();
	}
	
	// Update is called once per frame
	void Update() {

	}
	
	public void AddBlock() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			Vector3 point = hit.point + 0.5f * hit.normal;
			int newposx = (int) Mathf.Floor (point.x / size) * size; 
			int newposy = (int) Mathf.Floor (point.y / size) * size; 
			int newposz = (int) Mathf.Floor (point.z / size) * size;
			//placement boundaries
			//
			if(newposx>=0 && newposx < 16 && newposy>=0 && newposy < 16 && newposz>=0 && newposz < 16){
				for (int x = 0; x< size; x++) {
					for (int y = 0; y< size; y++) {
						for (int z = 0; z< size; z++) {
							array[(int)newposx+x,(int)newposy+y,(int)newposz+z] = color;
						}
					}
				}
				DrawArray ();

			}
		}
	}

	public void RemoveBlock() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			Vector3 point = hit.point - 0.1f * hit.normal;
			int newposx = (int) Mathf.Floor (point.x / size) * size; 
			int newposy = (int) Mathf.Floor (point.y / size) * size; 
			int newposz = (int) Mathf.Floor (point.z / size) * size;
			//placement boundaries
			//
			if(newposx>=0 && newposx < 16 && newposy>=0 && newposy < 16 && newposz>=0 && newposz < 16){
				for (int x = 0; x< size; x++) {
					for (int y = 0; y< size; y++) {
						for (int z = 0; z< size; z++) {
							array[newposx+x,newposy+y,newposz+z].a = 0;
							array2[(newposx+x)/4,(newposy+y)/4,(newposz+z)/4].a = 0;
						}
					}
				}
				DrawArray ();
			}
		}
	}

	public void DrawArray(){
		foreach (Transform child in body.transform) {
			GameObject.Destroy(child.gameObject);
		}
		for (int x = 0; x< 4; x++) {
			for (int y = 0; y< 4; y++) {
				for (int z = 0; z< 4; z++) {
					int x4 = x*4;
					int y4 = y*4;
					int z4 = z*4;
					bool large = true;
					for (int x2 = 0; x2< 4; x2++) {	
						for (int y2 = 0; y2< 4; y2++) {
							for (int z2 = 0; z2< 4; z2++) {
								if(array[x4+x2,y4+y2,z4+z2].a == 0 || array[x4+x2,y4+y2,z4+z2] != array[x4,y4,z4]){
									large = false;goto next;
								}
							}
						}
					}
				next:
					if(large){
						array2[x,y,z] = array[x4,y4,z4]; 
						DrawBlock(x, y, z, true);
					}
					else{
						for (int x2 = 0; x2< 4; x2++) {
							for (int y2 = 0; y2< 4; y2++) {
								for (int z2 = 0; z2< 4; z2++) {
									if(array[x4+x2,y4+y2,z4+z2].a != 0){
										DrawBlock(x4+x2, y4+y2, z4+z2, false);
									}
								}
							}
						}
					}
				}
			}
		}
	}

	private void DrawBlock(int x, int y, int z, bool large){
		int blocksize = 1;
		Color[,,] temparray = array;
		if (large) {
			blocksize = 4;	
			temparray = array2;
		}
		float cenx = x + 0.5f;
		float ceny = y + 0.5f;
		float cenz = z + 0.5f;
		Color currentcolor = temparray [x, y, z];
		if(y == 15/blocksize){
			DrawQuad(cenx,ceny+0.5f,cenz, 90, 0, currentcolor, blocksize);
		} else if(temparray[x,y+1,z].a == 0) {								
			DrawQuad(cenx,ceny+0.5f,cenz, 90, 0, currentcolor, blocksize);
		}
		if(y == 0){
			DrawQuad(cenx,ceny-0.5f,cenz, -90, 0, currentcolor, blocksize);
		} else if(temparray[x,y-1,z].a == 0){
			DrawQuad(cenx,ceny-0.5f,cenz, -90, 0, currentcolor, blocksize);
		}
		if(x == 15/blocksize){
			DrawQuad(cenx+0.5f,ceny,cenz, 0,-90, currentcolor, blocksize);
		}else if(temparray[x+1,y,z].a == 0){
			DrawQuad(cenx+0.5f,ceny,cenz, 0,-90, currentcolor, blocksize);
		}
		if(x == 0){
			DrawQuad(cenx-0.5f,ceny,cenz, 0,90, currentcolor, blocksize);
		}else if(temparray[x-1,y,z].a == 0){
			DrawQuad(cenx-0.5f,ceny,cenz, 0,90, currentcolor, blocksize);
		}
		if(z == 15/blocksize){
			DrawQuad(cenx,ceny,cenz+0.5f, 0,180, currentcolor, blocksize);
		}else if(temparray[x,y,z+1].a == 0){
			DrawQuad(cenx,ceny,cenz+0.5f, 0,180, currentcolor, blocksize);
		}
		if(z == 0){
			DrawQuad(cenx,ceny,cenz-0.5f, 0,0, currentcolor, blocksize);
		}else if(temparray[x,y,z-1].a == 0){
			DrawQuad(cenx,ceny,cenz-0.5f, 0,0, currentcolor, blocksize);
		}
	}

	private void DrawQuad(float xoff, float yoff, float zoff, int xang, int yang, Color currentcolor, int blocksize){
		GameObject newquad = Instantiate(quad,new Vector3(xoff,yoff,zoff)*blocksize, Quaternion.Euler (xang,yang,0)) as GameObject;
		newquad.renderer.material.color = currentcolor;
		newquad.transform.localScale *= blocksize;
		newquad.transform.parent = body.transform;
	}
}
