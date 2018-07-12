using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistractorManager : MonoBehaviour {

	private Image image;

	// Use this for initialization
	void Start () {
		GameObject nightGO = GameObject.Find ("Night");
		image = nightGO.GetComponent<Image> ();
		StartCoroutine(changeDayNight());
	}
	
	IEnumerator changeDayNight()
	{
		Nuit ();
		print(Time.time);
		yield return new WaitForSeconds(5);
		print(Time.time);
		Jour ();
		yield return new WaitForSeconds(1);
		print(Time.time);
		Nuit ();
		yield return new WaitForSeconds(1);
		print(Time.time);
		Jour ();
		yield return new WaitForSeconds(1);
		print(Time.time);
		StartCoroutine(changeDayNight());
	}


	void Jour() {
		image.enabled = false;
	}

	void Nuit() {
		image.enabled = true;
	}
}
