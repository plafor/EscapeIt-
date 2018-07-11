using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class InitReinforcer : MonoBehaviour {

	private static Sprite[] sprites;

	private List<Elem> elems; 

	public string imgBase;

	void Awake() {
		if (imgBase == null || imgBase == "")
			imgBase = "babydino";
		sprites = Resources.LoadAll<Sprite>("Sprites/Reinforcer/" + imgBase);
	}

	// Use this for initialization
	void Start () {

		loadData ();

		addElems ();



	}


	private void loadData() {



		if (File.Exists (Application.persistentDataPath + "/savedGames.gd")) {
			print ("save data found");
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
			elems = (List<Elem>)bf.Deserialize (file);
			file.Close ();
		} else {
			elems = new List<Elem> ();
			Elem anElem = new Elem ();
			anElem.name = "3";
			Vector3 pos = Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f)).origin;
			anElem.posX = pos.x;
			anElem.posY = pos.y;
			anElem.scaleX = 1.0f;
			anElem.scaleY = 1.0f;
			elems.Add (anElem);
		}

	}

	private void saveData() {
		updateData ();
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd");
		bf.Serialize(file, elems);
		file.Close();

	}

	private void addElem(Elem e) {
		print ("adding GameObject");
		GameObject parent = GameObject.Find ("parent");
		GameObject elem = Instantiate(Resources.Load("Prefabs/Reinforcers/babydino")) as GameObject; 
		elem.transform.parent = parent.transform;
		elem.name = "" + e.name;
		Vector3 pos = Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f)).origin;
		elem.transform.position = new Vector3 (pos.x, pos.y, 1f);
		elem.transform.localScale = new Vector3 (4f, 4f, 1f);
		elem.GetComponent<SpriteRenderer> ().sprite = sprites[int.Parse(e.name)];
	}

	private void addElems() {
		foreach (Elem e in elems) {
			addElem (e);
		}
	}

	private void updateData() {
		foreach (Elem e in elems) {
			GameObject go = GameObject.Find (e.name);
			e.posX = go.transform.position.x;
			e.posY = go.transform.position.y;
			e.scaleX = go.transform.localScale.x;
			e.scaleY = go.transform.localScale.y;
		}
	}

	void OnApplicationQuit()
	{
		saveData ();
	}

	[System.Serializable]
	class Elem {
		public string name;
		public float posX;
		public float posY;
		public float scaleX;
		public float scaleY;
	}

}


