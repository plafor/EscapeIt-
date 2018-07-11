using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisconnectButton : MonoBehaviour {

    public void Disconnect()
    {
        Debug.Log("Disocnnect()");
        ProfileManager.Disconnect();
        LoadProfileChoice();
    }

    public void LoadProfileChoice()
    {
        StartCoroutine(LoadProfileChoiceScreen());
    }

    IEnumerator LoadProfileChoiceScreen()
    {
        yield return true;
        new WaitForSeconds(1.0f); //wait time
        SceneManager.LoadScene(8);
    }
}
