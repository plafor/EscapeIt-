using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerStructure : MonoBehaviour {

	public Transform lockedLevelFab;
	public Transform currentLevelFab;
	public Transform finishedLevelFab;
	public Transform barsFab;
	public Transform bxSkillFab;


	// Use this for initialization
	void Start () {

		int maxLevels = 5;  // fictive values for testing
		int currentLevel = 1;

		if (GameObject.FindObjectOfType<GameManager> () != null) {
			maxLevels = GameObject.FindObjectOfType<GameManager> ().maxScenes;
			currentLevel = GameObject.FindObjectOfType<GameManager> ().currentScene + 1;
		}

		for (int i = 1; i <= maxLevels; i++) {
            Vector3 pos = GameObject.Find ("level" + i).transform.position;
			if (i == currentLevel) {
                //Instantiate (currentLevelFab, pos, Quaternion.identity);
                Instantiate(currentLevelFab, new Vector3(GetPos(i, maxLevels),pos.y, pos.z), Quaternion.identity);
            } else if (i < currentLevel) {
                //Instantiate (finishedLevelFab, pos, Quaternion.identity);
                Instantiate(finishedLevelFab, new Vector3(GetPos(i, maxLevels), pos.y, pos.z), Quaternion.identity);
            } else {
                //Instantiate (lockedLevelFab, pos, Quaternion.identity);
                Instantiate(lockedLevelFab, new Vector3(GetPos(i, maxLevels),pos.y, pos.z), Quaternion.identity);
            }
			if (i != maxLevels) {
				float posX = (GetPos(i+1, maxLevels) + GetPos(i, maxLevels)) / 2.0f;
				Instantiate(barsFab, new Vector3(posX, pos.y, pos.z), Quaternion.identity);
			}
			Vector3 posBx = new Vector3 (GetPos(i, maxLevels), pos.y + 3, pos.z);
			bxSkillFab.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
			string skill = GameObject.FindObjectOfType<GameManager> ().levels [i-1].solutionObjects [0].targetedSkill;
			print ("skill = " + skill);
			bxSkillFab.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Sprites/structure screen/" + skill);
			Instantiate (bxSkillFab, posBx, Quaternion.identity);

		}


	}
    public float GetPos(int i, int maxLevels)
    {
        float resul = 0.0f;
        switch (maxLevels)
        {
            case 3: resul = (GameObject.Find("level" + (i + 1)).transform.position.x + GameObject.Find("level" + i).transform.position.x) / 2.0f;
                break;
            case 4: resul = GameObject.Find("level" + i).transform.position.x;
                break;
            case 5: resul = (GameObject.Find("level" + (i - 1)).transform.position.x + GameObject.Find("level" + i).transform.position.x) / 2.0f;
                break;
        }
        return resul;
    }
	

}
