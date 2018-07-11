using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuidanceButton : MonoBehaviour
{
    public bool help;
    public GameManager manager;

    void Start()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
        help = false;
    }


    public bool HasBeenHelped()
    {
        return help;
    }

    public void SetHelp(bool b)
    {
        help = b;
    }
    public void FeedBack()
    {
        if (help)
        {
            Downgrade();
        }else
        {
            UpdateCurrentProfile();
        }
    }


    public void UpdateCurrentProfile()
    {
        string target = "";
        Debug.Log(manager.currentScene / manager.maxScenes);
        target = manager.levels[manager.currentScene-1].solutionObjects[0].targetedSkill;

        switch (target)
        {
            case "B3":
                Authentication.currentProfile.GetB3().IncConsecutiveSuccess();
                break;
            case "B4":
                Authentication.currentProfile.GetB4().IncConsecutiveSuccess();
                break;
            case "B8":
                Authentication.currentProfile.GetB8().IncConsecutiveSuccess();
                break;
            case "B9":
                Authentication.currentProfile.GetB9().IncConsecutiveSuccess();
                break;
            case "B13":
                Authentication.currentProfile.GetB13().IncConsecutiveSuccess();
                break;
            case "B19":
                Authentication.currentProfile.GetB19().IncConsecutiveSuccess();
                break;
            case "B25":
                Authentication.currentProfile.GetB25().IncConsecutiveSuccess();
                break;
        }

        ProfileManager.Update();
    }

    public void Downgrade()
    {
        string target = "";
        Debug.Log(manager.currentScene / manager.maxScenes);
        target = manager.levels[manager.currentScene - 1].solutionObjects[0].targetedSkill;

        switch (target)
        {
            case "B3":
                Authentication.currentProfile.GetB3().consecutiveSuccess = 0;
                break;
            case "B4":
                Authentication.currentProfile.GetB4().consecutiveSuccess = 0;
                break;
            case "B8":
                Authentication.currentProfile.GetB8().consecutiveSuccess = 0;
                break;
            case "B9":
                Authentication.currentProfile.GetB9().consecutiveSuccess = 0;
                break;
            case "B13":
                Authentication.currentProfile.GetB13().consecutiveSuccess = 0;
                break;
            case "B19":
                Authentication.currentProfile.GetB19().consecutiveSuccess = 0;
                break;
            case "B25":
                Authentication.currentProfile.GetB25().consecutiveSuccess = 0;
                break;
        }

        ProfileManager.Update();
    }



    public void NextScene()
    {
        StartCoroutine(LoadNextScene());
    }
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(2.0f); // wait time
        GameManager manager = GameObject.FindObjectOfType<GameManager>();
        if (manager.currentScene <= manager.maxScenes)
        {
            SceneManager.LoadScene("GameStructure");
        }
        else
        {
            Destroy(manager);
            SceneManager.LoadScene("LoaderGameManager");
        }
    }


}