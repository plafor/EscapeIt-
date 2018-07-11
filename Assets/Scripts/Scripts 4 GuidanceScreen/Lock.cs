using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lock : MonoBehaviour {

    public Text codeText;
    public InputField input;
    public GameObject panel;
    public GameObject dgPanel;
    public GameObject guidancePanel;

    private string pwd;

    // Use this for initialization
    void Start()
    {
        pwd = GetPwd();
    }

    // Update is called once per frame
    void Update()
    {

        if (input.text == pwd)
        {
            panel.SetActive(false);
            Unlock();
        }
    }
    public void Write(string s)
    {
        input.text += s;
    }
    public void Erase()
    {
        input.text = input.text.Substring(0, input.text.Length - 1);
    }
    public void EraseAll()
    {
        input.text = "";
    }
    public string GetPwd()
    {
        string resul;
        Preferences pref = XMLManager.LoadPreferences();
        resul = pref.GetCode();
        return resul;
    }
    public void Unlock()
    {
        panel.SetActive(false);
        dgPanel.SetActive(false);
        guidancePanel.SetActive(true);
    }

}
