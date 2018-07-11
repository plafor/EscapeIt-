using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileCreator : MonoBehaviour {

    //attributs
    public InputField input;
    public ProfileScrollList pList;


	public void OnClick()
    {
        Profile p = new Profile(input.text);
        ProfileManager.AddProfile(p);
        ProfileManager.Save();
        pList.RefreshDisplay();
        Preferences prefs = new global::Preferences();
        XMLManager.SavePreferences(prefs, p.GetId());
    }

}
