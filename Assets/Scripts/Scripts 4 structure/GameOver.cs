using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public GameObject panel;
    private GameManager manager;
    private bool end;
	// Use this for initialization
	void Start () {
        manager = GameObject.FindObjectOfType<GameManager>();
        end = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(manager.currentScene == manager.maxScenes && !end)
        {
            end = true;
            OnScenarioOver();

        }
	}

    public void OnScenarioOver()
    {
        //afficher le panel
        panel.SetActive(true);
    }



}
