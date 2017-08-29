using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement;


public class mainMenu : MonoBehaviour {

    //Main Menu
    public Button playText;
    public Button optionsText;
    public Button quitText;

    public Canvas quitMenu;
    public Canvas optionsMenu;

    // Level load progress
    public GameObject LoadingScene;
    public Slider LoadingBar;

    //Options Menu
    public GameObject optionsBackSelection;
    public Slider musicVolume;
    public Slider sfxVolume;
    private float sfxPreviousVolume;
    public AudioClip sfxTest;

    //Quit Menu
    public GameObject quitNoSelection;

    // Event System
    public EventSystem menuEventSystem;

    private void OnDisable() // When we switch off main menu save all the info
    {
        saveData();
        GameManager.Instance.saveData();
    }

    // Use this for initialization
    private void Start()
    {
        // Make sure we have the right volumem settings from the Game Manager
        loadData();

        //quitMenu = quitMenu.GetComponent<Canvas>();
        //optionsMenu = optionsMenu.GetComponent<Canvas>();
        playText = playText.GetComponent<Button>();
        optionsText = optionsText.GetComponent<Button>();
        quitText = quitText.GetComponent<Button>();

        quitMenu.enabled = false; // Disables the quit menu unless we call for it
        optionsMenu.enabled = false; // Disables the options menu unless we call for it

        sfxPreviousVolume = sfxVolume.value; // Sets it up so we don't hear the SFX sound on menu open
    }

    private void Update()
    {
        if (optionsMenu.enabled == true)
        {
            // Sets the sound levels
            AudioManager.Instance.musicVolume = musicVolume.value;
            if (sfxVolume.value != sfxPreviousVolume)
            {
                AudioManager.Instance.RandomSFX(sfxTest);
            }
            sfxPreviousVolume = sfxVolume.value;
            AudioManager.Instance.sfxVolume = sfxVolume.value;
            AudioManager.Instance.SetAudioLevels();
        }

        // If we move the joystick when no menu items are selected the joystick should take over again
        if ((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) && (menuEventSystem.currentSelectedGameObject == null || !menuEventSystem.currentSelectedGameObject.activeSelf))
        {
            if (playText.enabled == true)
                menuEventSystem.SetSelectedGameObject(menuEventSystem.firstSelectedGameObject);
            else if (optionsMenu.enabled == true)
                menuEventSystem.SetSelectedGameObject(optionsBackSelection);
            else if (quitMenu.enabled == true)
                menuEventSystem.SetSelectedGameObject(quitNoSelection);
        }

    }

    public void DisableEverything()
    {
        // Menus
        quitMenu.enabled = false;
        optionsMenu.enabled = false;

        // Text buttons
        playText.enabled = false;
        optionsText.enabled = false;
        quitText.enabled = false;
    }

    public void QuitPress() // Pulls up the quit confirmation menu
    {
        DisableEverything();
        quitMenu.enabled = true;
        menuEventSystem.SetSelectedGameObject(quitNoSelection);
    }

    public void OptionsPress() // Pulls up the options menu
    {
        DisableEverything();
        optionsMenu.enabled = true;
        menuEventSystem.SetSelectedGameObject(optionsBackSelection);
    }

    public void OptionsBack() // Goes from options back to main menu
    {
        DisableEverything();
        playText.enabled = true;
        optionsText.enabled = true;
        quitText.enabled = true;
        menuEventSystem.SetSelectedGameObject(menuEventSystem.firstSelectedGameObject);
    }

    public void NoQuit() // No on the quit menu
    {
        quitMenu.enabled = false;
        playText.enabled = true;
        quitText.enabled = true;
        optionsText.enabled = true;
        menuEventSystem.SetSelectedGameObject(menuEventSystem.firstSelectedGameObject);
    }


    public void StartGame()
    {
        DisableEverything();
        StartCoroutine(LevelCoroutine()); // Runs the level loading coroutine
        //AsyncOperation async = Application.LoadLevelAsync(1);
        //Application.LoadLevel(1);
    }

    IEnumerator LevelCoroutine()
    {
        LoadingScene.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(1); // loads our level asynchronously (Which means it will not proceed until it receives a signal of completion)

        while (!async.isDone)
        {
            //Debug.Log(async.progress);
            LoadingBar.value = async.progress / 0.9f;
            yield return null;
        }
        //yield return null;
    }

    public void PointerEnter() // Same as exit just written in two ways for learning
    {
        if (menuEventSystem.currentSelectedGameObject != null)
            menuEventSystem.SetSelectedGameObject(null);
    }

    // Deselects 
    public void PointerExit()
    {
        if (menuEventSystem.currentSelectedGameObject != null)
            menuEventSystem.SetSelectedGameObject(null);
    }

    public void saveData()
    {
        GameManager.Instance.musicVolume = musicVolume.value;
        GameManager.Instance.sfxVolume = sfxVolume.value;
    }

    public void loadData()
    {
        musicVolume.value = GameManager.Instance.musicVolume;
        sfxVolume.value = GameManager.Instance.sfxVolume;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
