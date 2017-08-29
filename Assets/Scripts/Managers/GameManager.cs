using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool phaiseShift;
    public bool slowdown; // Variable used to store a temporary game slowdown
    private float phaiseSlowdown; // How long it takes to go back to full time speed
    private float phaiseSlowdownTotal = 0.5f;

    public bool isDead; // Does nothing atm
    public int EnemyCount; //{ get; set; } // Counts how many enemies are in the game
    public bool noEnemies = false; // Sets it up so we don't spam check for doors each frame
    public string location;

    // Volume
    public float globalVolume = 100;
    public float musicVolume;
    public float sfxVolume;

    //UI Updates
    public GameObject keyUI;
    public int KeyCount = 0; // Counts how many keys we have
    public GameObject currency;
    public int CurrencyCount = 1; // Counts how many keys we have

    // Difficulty Variables
    public float difficulty;

    // Visual effects
    public bool screenWhite = false;

    //Saving and Loading
    private bool saveFileExists;

    void Awake()
    {
        loadData();
        if (!saveFileExists)
        {
            musicVolume = 1;
            sfxVolume = 1;
        }

        difficulty = 1f; // Start us off with an easier difficulty
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject); (MUST ATTACH TO ROOT MANAGER OBJECT I GUESS)

        phaiseSlowdown = phaiseSlowdownTotal;
        AudioListener.volume = 0;
        location = "Room1";

    }

    void Start()
    {

    }

    void Update()
    {
        // Screen fade to white 
        if (screenWhite == true)
        {
            if (gameObject.GetComponent<ScreenFader>().fadeIn == true)
            {
                gameObject.GetComponent<ScreenFader>().fadeIn = false;
            }
            else
                gameObject.GetComponent<ScreenFader>().fadeIn = true;
            screenWhite = false;
        }
        

        if (AudioListener.volume < globalVolume / 100 && AudioListener.volume < 1) // Ease in the games volume upon load (Plus safety check so we never go above 1 volume)
        {
            AudioListener.volume += (globalVolume / 100) / 100; // Eases the volume in 1% per frame
        }

        // PROBABLY DELETE
        if (slowdown == true && phaiseSlowdown > 0)
        {
            Time.timeScale = 0.1f;
            phaiseSlowdown -= Time.unscaledDeltaTime;
        }
        else
        {
            slowdown = false;
            Time.timeScale = 1f;
            phaiseSlowdown = phaiseSlowdownTotal;
        }
    }


    public void volumeAdjustment()
    {
        AudioListener.volume = globalVolume / 100;
    }

    public void keyUpdate()
    {
        if (keyUI == null)
            keyUI = GameObject.Find("keys");

        keyUI.GetComponent <Text>().text = "Keys: " + KeyCount.ToString(); // Sets the text for the key count
    }

    public void currencyUpdate()
    {
        if (currency == null)
            currency = GameObject.Find("Currency");

        currency.GetComponent<Text>().text = "Currency: " + CurrencyCount.ToString(); // Sets the text for the key count
    }

    public void saveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/options.banana");

        SavedData data = new SavedData();
        data.savedMusicLevel = musicVolume;
        data.savedSFXLevel = sfxVolume;
        bf.Serialize(file,data);
        file.Close();
        
    }

    public void loadData()
    {
        
        if (File.Exists(Application.persistentDataPath + "/options.banana"))
        {
            saveFileExists = true;
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/options.banana",FileMode.Open);
            SavedData data = (SavedData)bf.Deserialize(file);
            file.Close();

            musicVolume = data.savedMusicLevel;
            sfxVolume = data.savedSFXLevel;
        }
        //Debug.Log("Music:" + musicVolume);
    }
}

[Serializable]
class SavedData
{
    public float savedMusicLevel;
    public float savedSFXLevel;
}