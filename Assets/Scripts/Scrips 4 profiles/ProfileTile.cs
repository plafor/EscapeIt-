using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ProfileTile : MonoBehaviour {

    public Text profileName;
    public Button button;

    private Profile profile;
    private ProfileScrollList profileScrollList;
    

    public void SetUp(Profile p, ProfileScrollList currentScrollList)
    {
        profile = p;
        profileName.text = p.GetName();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites/Profile/animals");
        button.image.sprite = sprites[p.GetSprite()];
        profileScrollList = currentScrollList;
    }

    public void LogIn()
    {
        ProfileManager.Connect(profile);
        GameManager manager = GameObject.FindObjectOfType<GameManager>();
        manager.childName = profile.GetName();
    }

}
