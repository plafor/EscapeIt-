using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class LoadScenariosFromResources : MonoBehaviour {

	// Use this for initialization
	void Start () {

		string[] files = null;
		//#if UNITY_EDITOR
		//files = Directory.GetFiles("Assets/Resources/XMLFiles");
		//#endif
		//#if UNITY_ANDROID
		string path= Application.persistentDataPath + "/XMLFiles/";
		DirectoryInfo dataDir = new DirectoryInfo (path);
		try {
			FileInfo[] fileinfo = dataDir.GetFiles ();
			files = new string[fileinfo.Length];
			for (int i=0; i<fileinfo.Length; i++) {
				string name = fileinfo [i].Name;
				files[i] = name;
				Debug.Log("name  "+name);
			}
		} catch (System.Exception e) {
			Debug.Log (e);
		}
		//string[] files = Directory.GetFiles("Assets/Resources/XMLFiles");
		//#endif

		foreach (string file in files)
		{ 
			if (!file.EndsWith (".xml"))
				continue;
			print (file);
			GameObject elem = Instantiate(Resources.Load("Prefabs/GameMenu/Button")) as GameObject; 
			Button button = elem.GetComponent<Button>();


			string[] tokens = file.Split ('/');

			string lastToken = tokens[tokens.Length - 1];
			string name = lastToken.Split ('.') [0];

			button.onClick.AddListener(() => { action(name); });

			elem.name = name;

			elem.transform.Find ("Text").GetComponent<Text>().text = name;

			elem.transform.SetParent (gameObject.transform);
		}
	}

	public void action(string name) {
		print (name);

		SoundManagerScript.PlaySound (SoundManagerScript.STARTING);
		GameManager manager = GameObject.FindObjectOfType<GameManager> ();

		//#if UNITY_EDITOR
		//TextAsset fileToLoad = Resources.Load("XMLFiles/" + name) as TextAsset;
		//manager.xmlLocalFile = fileToLoad;
		//#endif
		//#if UNITY_ANDROID
		manager.localFileName4Android = name;
		//#endif

		manager.LoadMyXML ();
		StartCoroutine(LoadNextScene());


	}


	IEnumerator LoadNextScene(){
		yield return new WaitForSeconds(1.0f); // wait time
		SceneManager.LoadScene("GameStructure");

	}

}
