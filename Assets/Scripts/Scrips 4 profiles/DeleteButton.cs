using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteButton : MonoBehaviour {

    public void Delete()
    {
        Debug.Log("Delete()");
        ProfileManager.Delete();
        LoadProfileChoice();
    }

    public void LoadProfileChoice()
    {
        StartCoroutine(LoadProfileChoiceScreen());
    }

    IEnumerator LoadProfileChoiceScreen()
    {
        yield return true; //new WaitForSeconds(1.0f); // wait time
        SceneManager.LoadScene("ProfileChoice");
    }
}
