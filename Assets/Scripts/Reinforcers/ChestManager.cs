using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ChestManager : MonoBehaviour {

	private static Sprite[] sprites;
	private static int nbSprites;
	private GameObject newObject;
	public  string imgBase;
	public bool exit = false;

	private string saveFileName;

	private List<int> elems;


	private void loadData() {
		if (File.Exists (Application.persistentDataPath + saveFileName)) {
			print ("load data");
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + saveFileName, FileMode.Open);
			elems = (List<int>)bf.Deserialize (file);
			/*for(int i = 0; i < elems.Count; i++)
				print(elems[i]);*/
			file.Close ();
			if (elems.Count == 0)
				resetList();
		} else {
			resetList ();
		}
	}

	private void resetList() {
		elems = new List<int> ();
		for(int i = 0; i < nbSprites; i++)
		{
			elems.Add (i);
		}
	}

	private void saveData() {
		print ("save data");
		/*for(int i = 0; i < elems.Count; i++)
			print(elems[i]);*/
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + saveFileName);
		bf.Serialize(file, elems);
		file.Close();

	}

	void Awake() {
		if (imgBase == null || imgBase == "")
			imgBase = "babydino";
		saveFileName =  "/XMLFiles/Reinforcers/reinforce" +Authentication.currentProfile.GetId()+imgBase + "Save.save";
		sprites = Resources.LoadAll<Sprite>("Sprites/Reinforcer/" + imgBase);
		nbSprites = sprites.Length;
		print ("nb sprites = " + sprites.Length);
		if (sprites.Length == 0) {
			if (imgBase == "starwar") {
				nbSprites = 21;
				sprites = new Sprite[21];
			}
		}
		loadData ();
		displayFoundSprites ();
	}

	private void displayFoundSprites() {
		for(int i = 0; i < elems.Count; i++) {
			GameObject sprite = GameObject.Find (imgBase + "_" + elems[i]);
			sprite.GetComponent <SpriteRenderer> ().color = Color.black;
		}
	}

	public void actionChest () {
		exit = false;
		GameObject chest = GameObject.Find ("Chest");
		Animator animator = chest.GetComponent<Animator> ();
		animator.enabled = true;
		SoundManagerScript.PlaySound (SoundManagerScript.REINFORCERCHEST);
        StartCoroutine(waiter());
	}


	IEnumerator waiter()
	{
		yield return new WaitForSeconds(1);
		GameObject anim = GameObject.Find ("Animation");
		anim.SetActive (false);
		int random = pickRandomSprite ();

		saveData ();

		newObject = GameObject.Find (imgBase + "_" + random);

		hideMyself ();

		StartCoroutine(end());

		newObject.GetComponent <FoundScript>().foundSpriteAnim ();


	}

	IEnumerator end()
	{
		
		print ("remaining = " + elems.Count);
		if (elems.Count == 0) {
			print ("sound");
			yield return new WaitForSeconds(4);
			print ("end pause");
			SoundManagerScript.PlaySound (SoundManagerScript.REINFORCERCOMPLETE);
        }
		exit = true;
	}

	public int pickRandomSprite() {
		int index = Random.Range(0, elems.Count);
		int res = elems [index];
		elems.Remove (res);
		return res;
	}

	public void instantiateSprite (int num) {
		
		GameObject elem = Instantiate(Resources.Load("Prefabs/Reinforcers/" + imgBase)) as GameObject; 

		elem.name = "" + num;
		Vector3 pos = Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f)).origin;
		elem.transform.position = new Vector3 (pos.x, pos.y, 1f);
		elem.transform.localScale = new Vector3 (3f, 4f, 1f);
		elem.GetComponent<SpriteRenderer> ().sprite = sprites[num];
	}

	public void hideMyself() {
		gameObject.GetComponent<SpriteRenderer>().enabled = false;
	}
}
