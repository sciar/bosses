using UnityEngine;
using System.Collections;

public class pauseMenu : MonoBehaviour {

    private bool pause;

    // Update is called once per frame
    private void Update()
    {   
        if (Input.GetButtonDown("Pause"))
        {
            Debug.Log(Time.timeScale);
            if (!pause)
            {
                Time.timeScale = 0;
                pause = true;
            }
            else
            {
                Time.timeScale = 1;
                pause = false;
            }
            
        }
    }
    
}
