using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LockSetter : MonoBehaviour {

    public GameObject panel;
    public SettingsManager sm;
    public Image img1;
    public Image img2;
    public Image img3;
    public Image img4;

    private string input;
    private string pwd;
    // Use this for initialization
    void Start()
    {
        input = "";
        Display();
        pwd = GetPwd();

    }

    // Update is called once per frame
    void Update()
    {
        if (input.Length == 4)
        {
            panel.SetActive(false);
            sm.code.text = input;
            input = "";
            sm.SavePreferences();
        }
    }
    public void Write(string s)
    {
        if (input.Length < 4)
        {
            input += s;
        }
        Debug.Log(input);
        Display();
    }
    public void Erase()
    {
        input = input.Substring(0, input.Length - 1);
        Display();
    }
    public void EraseAll()
    {
        input = "";
        Display();
    }
    public string GetPwd()
    {
        string resul;
        Preferences pref = XMLManager.LoadPreferences();
        resul = pref.GetCode();
        return resul;
    }
    public void Display()
    {
        img1.enabled = false;
        img2.enabled = false;
        img3.enabled = false;
        img4.enabled = false;
        if (input.Length > 0)
        {
            img1.enabled = true;
        }
        if (input.Length > 1)
        {
            img2.enabled = true;
        }
        if (input.Length > 2)
        {
            img3.enabled = true;
        }
        if (input.Length > 3)
        {
            img4.enabled = true;
        }

    }
}
