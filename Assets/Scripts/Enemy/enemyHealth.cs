using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class enemyHealth : MonoBehaviour {

    private Animator anim;

    public float startingHealth = 100f; // This mobs total health
    public Slider slider; // This is the variable to access the slider attached to this object
    public Image fillImage; // This is the image component that fills the slider
    public Color fullHealthColor = Color.green;
    public Color zeroHealthColor = Color.red;

    public float currentHealth; // The current value of hp
    private bool dead;

    public int damageValue = 0;

    // Info for spawning Phaise version of an enemy
    private GameObject phaiseVersion; // GameObject that will hold the phaise version of the monster

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>(); // Grabs the animator attached to the object

        phaiseVersion = (GameObject)Resources.Load("Prefabs/Enemies/" + this.gameObject.name +"Phaise"); // This should check the resources folder for the phaise version of each enemy
        //phaiseVersion = (GameObject)Resources.Load("Prefabs/Enemies/jellyPhaise");
        //Debug.Log(this.gameObject.name);
        //Debug.Log(phaiseVersion);
    }

    //When the enemy is created
    private void OnEnable()
    {
        currentHealth = startingHealth;
        dead = false;
        SetHealthUI();
    }

    public void TakeDamage(float amount)
    {
        //Adjust the enemies current health
        currentHealth -= amount;
        SetHealthUI();
        if (currentHealth <= 0f && !dead)
        {
            OnDeath();
        }
    }

    private void SetHealthUI()
    {
        // Update the UI with the current HP
        slider.value = currentHealth;

        fillImage.color = Color.Lerp(zeroHealthColor,fullHealthColor,currentHealth/startingHealth); // This is where we check how much is missing from full hp and adjust color
    }

    private void OnDeath()
    {
        dead = true; // Make sure we only call the death script once


        gameBehavior.shakeAmount = 0.1f;
        gameBehavior.screenShake = 20f; // Makes the screen shake for X frames

        //Debug.Log(phaiseVersion);
        // If we are on a non Phaise version spawn the Phaise of the monster
        if (phaiseVersion != null) // Check for not null either we lower the enemy count or spawn a new one
        {
            Instantiate(phaiseVersion, this.transform.position, Quaternion.identity); //phaiseVersion defined in start()
        }
        else
        {
            GameManager.Instance.EnemyCount--; // Subtracts from total enemies spawned right now
        }

        if (anim == null)
            Destroy(gameObject); // When the monster dies we remove it
        else
        {
            anim.SetBool("Walking", false); // Sets the animator to walk
            anim.SetBool("Attacking", false); // Stops attack animations
            anim.SetTrigger("Dead"); // And we die
            // Send bool dead = true to YetiAI (or mobname + "AI)"
        }
    }

	

	
	// Update is called once per frame
	void Update () {
        //float hitPause = Time.realtimeSinceStartup + 2f;
        

        if (damageValue > 0)
        {
            TakeDamage(damageValue);
            damageValue = 0;
        }
        
    }

}
