
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Help : MonoBehaviour {

    //private static int levelData = 0;
    public GameObject hint;
    public GameObject open;
	public void bigger() {

	//	levelData++;
	//	if (levelData == 1) {
			print ("help level 1 => selectable elements");


			GameObject[] liste = GameObject.FindGameObjectsWithTag ("selection");

			foreach (GameObject go in liste) {
				go.transform.GetComponent<SpriteRenderer> ().sortingLayerName = "draglayer";
			}
	/*	} else if (levelData == 2) {
			print ("help level 2 => selectable elements BIGGER");


			GameObject[] liste = GameObject.FindGameObjectsWithTag ("selection");

			foreach (GameObject go in liste) {
				go.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);

			}
		}  */
			
	}
    public void DisplayHelp()
    {
        hint.SetActive(!hint.active);
        open.SetActive(!open.active);
    }

    public void OpenDoors()
    {
        ManageExit[] liste = GameObject.FindObjectsOfType<ManageExit>();

        foreach (ManageExit exit in liste)
        {
            exit.OnTurnOn();
        }
    }

}
