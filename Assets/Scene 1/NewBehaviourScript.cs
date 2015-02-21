using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class NewBehaviourScript : MonoBehaviour {

	float health;
	float experience;
	// Use this for initialization
	void Start () {
		health = 0f;
		experience = 0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data = new PlayerData ();
		data.health = health;
		data.experience = experience;
		bf.Serialize (file, data);
		file.Close();
	}


	public void Load(){
		if(File.Exists (Application.persistentDataPath + "/playerInfo.dat")){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
		PlayerData data = (PlayerData)bf.Deserialize (file);

		health = data.health;
		experience = data.experience;
		file.Close();
		
		}
	}
}

[Serializable]
class PlayerData {
	public float health;
	public float experience;
}