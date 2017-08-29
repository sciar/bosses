using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class manualColorChange : MonoBehaviour {
    // http://docs.unity3d.com/ScriptReference/MonoBehaviour.html This is where you go to find all of these

    

    // Use this for initialization
    private void Start()
    {
        GetComponent<Image>().color = Color.red;
    }
}
