using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Authentication : MonoBehaviour {

    public static Profile currentProfile;

    public static void Connect(Profile p)
    {
        currentProfile = p;
    }
    public static void QuitCurrentProfile()
    {
        currentProfile = null;
    }
}
