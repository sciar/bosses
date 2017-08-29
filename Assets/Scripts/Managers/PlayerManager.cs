using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public int strength;
    public int speed;
    public static int maxHealth = 100;
    public static int maxStamina = 100;
    public playerMovement mainPlayer;

    public bool mhFunction;
    public bool msFunction;


    void Awake()
    {

        if (Instance != null)
        {
            GameObject.Destroy(this.gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(this); 

    }
    void Start()
    {
        strength = 1;
        speed = 6;
    }

    void Update()
    {
        if (mhFunction == true)
        {
            ModifyHealth();
            mhFunction = false;
        }
        if (msFunction == true)
        {
            ModifyStamina();
            msFunction = false;
        }
    }

    public void ModifyHealth() // Sets a new max health
    {
        //Debug.Log("Got here");
        GameObject player1 = GameObject.Find("Player");
        player1.GetComponent<playerHP>().maxHealth = maxHealth;
        player1.GetComponent<playerHP>().setHealth();
    }
    public void ModifyStamina() // Sets a new max stamina
    {
        GameObject player1 = GameObject.Find("Player");
        player1.GetComponent<playerHP>().maxStamina = maxStamina;
    }

    public void RegisterPlayer(playerMovement newPlayer) // playerMovement declares which player is main in this manager
    {
        mainPlayer = newPlayer;
    }

}
