using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundScript : MonoBehaviour {



	public void foundSpriteAnim() {
		GameObject animStar = Instantiate(Resources.Load("Prefabs/Reinforcers/AnimStar")) as GameObject; 
		animStar.transform.parent = gameObject.transform;
		animStar.transform.localPosition = new Vector3 (0f, 0f, -1f);

		StartCoroutine(waiter2());


	//instantiateSprite (random);


	}


	IEnumerator waiter2()
	{
		yield return new WaitForSeconds(2);

		GetComponent <SpriteRenderer> ().color = Color.white;
		SoundManagerScript.PlaySound (SoundManagerScript.REINFORCERNEWSPRITE);
	}

}
