using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerHP : MonoBehaviour {
    // http://docs.unity3d.com/ScriptReference/MonoBehaviour.html This is where you go to find all of these

    [SerializeField]
    private Slider slider; // This is the variable to access the slider attached to the screen canvas (Attach in Unity UI)
    public GameObject hpText; // This grabs the entire hpText component
    private Text hpUpdate; // This is the text info from the other object

    public float currentHealth;
    public float maxHealth = 100f;

    // Hit invincibility and flashing
    [SerializeField]
    private float hitInvincibility;
    private Color collideColor = Color.white;
    private Color normalColor;

    // Stamina
    public Slider staminaSlider;
    public float currentStamina;
    public float maxStamina = 100f;
    public float staminaCost = 0f; // How much moves cost in stamina
    private float staminaPause; // Used to temporarily halt the regeneration of stamina
    private float staminaGain = 0.35f;
    public bool forwardDodge = false;

    // Use this for initialization
    private void Start()
    {
        currentHealth = maxHealth;
        hpUpdate = hpText.GetComponent<Text>();
        currentStamina = maxStamina;

    }

    // Update is called once per frame
    void Update()
    {

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

            currentHealth -= damage;
            setHealth();
            hitInvincibility = 1f;
            Debug.LogError("Player took damage");
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
