using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class optionsMenu : MonoBehaviour {

    // Back button is handled by mainMenu.cs
    public Canvas optionMenu;
    public Canvas controlsMenu;
    public Button controls;
    public Button fullScreen;
    public Button controllerVibration;

    public void DisableEverything() // Saves time disabling everything
    {
        // Menus
        optionMenu.enabled = false;
        controlsMenu.enabled = false;
        // Text buttons
        
    }

    public void ControlsPress() // Pulls up the controls menu
    {
        DisableEverything();
        
    }

}
