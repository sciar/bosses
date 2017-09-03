using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerHP : MonoBehaviour {
    // http://docs.unity3d.com/ScriptReference/MonoBehaviour.html This is where you go to find all of these

    [Header("Health")]
    [SerializeField]
    private Slider slider; // This is the variable to access the slider attached to the screen canvas (Attach in Unity UI)
    public GameObject hpText; // This grabs the entire hpText component
    private Text hpUpdate; // This is the text info from the other object

    public float currentHealth;
    public float maxHealth; // -SET IN INSPECTOR

    // Hit invincibility and flashing
    [SerializeField]
    private float hitInvincibility;
    private Color collideColor = Color.white;
    private Color normalColor;

    // Stamina
    [Header("Stamina")]
    public Slider staminaSlider;
    public float currentStamina;
    public float maxStamina = 100f;
    public float staminaCost = 0f; // How much moves cost in stamina
    private float staminaPause; // Used to temporarily halt the regeneration of stamina
    private float staminaGain = 0.15f;
    public bool forwardDodge = false;

    [Header("Shield")]
    public Slider shieldSlider;
    public GameObject shieldText; // This grabs the entire shieldText component
    public float shieldValueCurrent;
    public GameObject shieldUIContainer;
    [SerializeField]
    private float shieldValueTotal; // We need this cause I'm tired so fuck
    [SerializeField]
    private Text shieldUpdate; // This is the text info from the other object

    // Use this for initialization
    private void Start()
    {
        currentHealth = maxHealth;
        hpUpdate = hpText.GetComponent<Text>();
        shieldUpdate = shieldText.GetComponent<Text>();
        currentStamina = maxStamina;
        shieldUIContainer.SetActive(false); // Makes sure this is off at first
    }

    // Update is called once per frame
    void Update()
    {
        if (shieldValueCurrent <= 0 && GetComponent<playerMovement>().frostShieldDuration > 0)
        {// If the shield is depleted but still on we tell the playerMovement script to turn it off
            GetComponent<playerMovement>().frostShieldDuration = 0;
        }

        if (currentHealth <= 0)
        {
            // You ded
            gameObject.SetActive(false);
        }

        if (hitInvincibility > 0) // Just a reducer if we have hit invincibility to make it go back down
        {
            hitInvincibility -= Time.deltaTime;
        }

        if (staminaCost > 0)
            useStamina();

        maxStamina = PlayerManager.maxStamina; // Sets the HP script maximum stamina to the same value as the managers stamina

        if (staminaPause <= 0 && currentStamina <= maxStamina && GetComponent<playerMovement>().dodging != true)
        {
            currentStamina += (maxStamina - currentStamina) * staminaGain * Time.deltaTime;
        }
        else if (staminaPause > 0)
            staminaPause -= Time.deltaTime;
        // Update stamina values each frame
        staminaSlider.value = (currentStamina / maxStamina) * 100;
    }

    // Functions to be used as Coroutines MUST return an IEnumerator
    IEnumerator Flasher()
    {
        for (int i = 0; i < 5; i++) // Makes us flash when we take damage
        {
            GetComponent<Renderer>().material.color = normalColor;
            yield return new WaitForSeconds(.1f);
            GetComponent<Renderer>().material.color = collideColor;
            yield return new WaitForSeconds(.1f);
        }
    }

    public void takeDamage(float damage)
    {
        if (hitInvincibility > 0)
        {
            // Don't hurt players during invincibility
        }
        else
        {
            //StartCoroutine(Flasher()); // Makes us blink

            // Store the shields value
            shieldValueTotal = shieldValueCurrent;
            // Remove damage from the shield
            shieldValueCurrent -= damage;
            damage -= shieldValueTotal; // Then we remove the value of the shield from the damage before calculating the damage to player
            // If damage is still left over we strip it from the health
            if (damage > 0)
            {
                currentHealth -= damage;
                hitInvincibility = 1f;
            }
            setHealth(); // Set the health regardless cause this will update the UI

            // Set the shield values if they're above 0
            if (shieldValueCurrent <= 0) { }
            else
            {
                frostShieldUpdate(); // Updates the values **NOT WORKING FUCK IF I KNOW WHY**
            } 
        }
        
    }

    public void frostShield(float strength)
    {
        // Set our sheild values to whatever this boost gave them
        shieldValueTotal = strength;
        shieldValueCurrent = strength;
        // Set the max value to whatever the shields total value currently is (The last value sent as the max)
        shieldSlider.maxValue = shieldValueTotal;
        frostShieldUpdate();
    }

    private void frostShieldUpdate()
    {
        if (shieldValueTotal == 0)
        {
            shieldUIContainer.SetActive(false);
        }
        else
        {
            shieldUIContainer.SetActive(true);
            //Debug.LogError("Current Value: " + shieldValueCurrent);
            //Debug.LogError("Total Value: " + shieldValueTotal);
            //Debug.LogError("Percentage it should show:" + Mathf.RoundToInt((shieldValueCurrent / shieldValueTotal) * 100));
            shieldSlider.value = (shieldValueCurrent / shieldValueTotal )* 100;
            //shieldSlider.value = Mathf.RoundToInt((shieldValueCurrent / shieldValueTotal) * 100);
            shieldUpdate.text = Mathf.CeilToInt(shieldValueCurrent).ToString() + "/ " + Mathf.FloorToInt(shieldValueTotal).ToString();
        }
    }

    public void setHealth()//int damageValue
    {
        slider.value = Mathf.RoundToInt((currentHealth/maxHealth) * 100);
        hpUpdate.text = Mathf.CeilToInt(currentHealth).ToString() + "/ " + Mathf.FloorToInt(maxHealth).ToString();
    }

    private void useStamina()
    {
        currentStamina -= staminaCost;
        if (forwardDodge)
        {
            staminaPause += 0.95f;
            forwardDodge = false;
        }
        else
            staminaPause += 0.65f;

        staminaCost = 0;
    }

}
