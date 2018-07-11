using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ProfileScrollList : MonoBehaviour {

    public List<Profile> profiles;
    public Transform contentPanel;

    public SimpleObjectPool profilePool;


	// Use this for initialization
	void Start () {
        RefreshDisplay();
	}
	

    public void RefreshDisplay()
    {
        RemoveButtons();  
        LoadFromXML();
        for (int i = 0; i < profiles.Count; i++)
        {
            Debug.Log(profiles[i]);
        }
        AddButtons();
    }


    private void AddButtons()
    {  
        for(int i = 0; i < profiles.Count; i++)
        {
            Profile p = profiles[i];
            GameObject newProfile = profilePool.GetObject();
            newProfile.transform.SetParent(contentPanel);

            ProfileTile profileTile = newProfile.GetComponent<ProfileTile>();
            profileTile.SetUp(p, this);
        }
    }
    private void RemoveButtons()
    {
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            profilePool.ReturnObject(toRemove);
        }

    }


    private void LoadFromXML()
    {
        profiles = ProfileManager.GetProfilesFromXML();

    }
    public void SaveToXML()
    {
        ProfileManager.Save();

    }

}
