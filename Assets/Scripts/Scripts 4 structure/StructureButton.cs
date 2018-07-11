using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StructureButton : MonoBehaviour {

    public void LoadReinforcerScene()
    {
        StartCoroutine(LoadReinforcerScreen());
    }


    IEnumerator LoadReinforcerScreen()
    {
        yield return true;

        Preferences pref = XMLManager.LoadPreferences();
        Reinforcer r = pref.GetReinforcer();
        switch (r)
        {
            case Reinforcer.dino:
                SceneManager.LoadScene("SceneReinforcerDino");
                break;
            case Reinforcer.farm:
                SceneManager.LoadScene("SceneReinforcerFarm");
                break;
            case Reinforcer.mario:
                SceneManager.LoadScene("SceneReinforcerMario");
                break;
            case Reinforcer.starwars:
                SceneManager.LoadScene("SceneReinforcerStarWars");
                break;
        }
           
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadMainMenuScreen());
    }
    IEnumerator LoadMainMenuScreen()
    {
        yield return true; //new WaitForSeconds(1.0f); // wait time
        SceneManager.LoadScene("Menu");
    }
}
