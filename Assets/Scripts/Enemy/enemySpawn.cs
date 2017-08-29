using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemySpawn : MonoBehaviour {

    private int totalEnemies;
    public bool spawnEnemies;

    private int difficulty; // Pulls from the GameManagers difficulty

    // List of all available enemies
    public GameObject tree; // This is the variable for declaring which enemy to spawn
    public GameObject jelly;
    public GameObject yeti;

    List<GameObject> enemyList = new List<GameObject>(); // Creates a list where we store all the enemies in the game

    public GameObject spawnPoint0;
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    public GameObject spawnPoint3;
    public GameObject spawnPoint4;
    public GameObject spawnPoint5;
    public GameObject spawnPoint6;
    public GameObject spawnPoint7;
    

    List<GameObject> spawnList = new List<GameObject>(); // Creates a list where we store all the enemies in the game
    List<int> spawnsUsedList = new List<int>(); // Keeps track of which spawn points have been taken already

    // Use this for initialization
    void Start () {
        //SpawnEnemies();  //TESTING TOOL TO MAKE EACH ROOM AUTO SPAWN ENEMIES
        
    }

    void Update()
    {
        if (spawnEnemies)
        {
            SpawnEnemies();
            spawnEnemies = false;
        }
    }
	
    void SpawnEnemies()
    {
        // Spawn Points
        spawnList.Add(spawnPoint0);
        spawnList.Add(spawnPoint1);
        spawnList.Add(spawnPoint2);
        spawnList.Add(spawnPoint3);
        spawnList.Add(spawnPoint4);
        spawnList.Add(spawnPoint5);
        spawnList.Add(spawnPoint6);
        spawnList.Add(spawnPoint7);

        totalEnemies = Random.Range(1, spawnList.Count+1); // Sets up how many enemies will spawn +1 since it starts at 0
        // Take the current amount of total enemies and multiply it by 20% of the current difficulty level (1 = 0.2 // 2 = 0.4 etc.)
        difficulty = Mathf.RoundToInt(GameManager.Instance.difficulty * 0.3f * totalEnemies);
        if (difficulty <= 1) // Safety check in case we get a 0
            difficulty = 1;

        totalEnemies = difficulty;

        //Debug.Log(totalEnemies);
        // Stores our list of available enemies with tiers for the current difficulty level
        if (difficulty < 2)
        {
            enemyList.Add(tree);
            enemyList.Add(jelly);
            enemyList.Add(yeti);
        }
        


        /* USEFUL SHIT FOR LISTS
        foreach (GameObject data in enemyList)
        {
            // This loops through the entire list and does whatever
        }
        Debug.Log(enemyList[0]); // Prints out list items based on when they get stored
        */

        for (int i = 0; i < totalEnemies; i++) // This spawns however many enemies we're going to be making
        {
            // First we get a number from the potential amount of spawn points
            int spawnToUse = Random.Range(0, spawnList.Count);
            // Then we check if it's in our "used" list of spawn points
            while (spawnsUsedList.Contains(spawnToUse))
            {
                // If it is we reroll until we get one that isn't used
                spawnToUse = Random.Range(0, spawnList.Count);
            }
            // Then when we're done we add whatever we got to the "used" list so we don't use it again
            spawnsUsedList.Add(spawnToUse);
            
            int enemyRandomizer = Random.Range(0, enemyList.Count); // Randomly picks an enemy from the list each loop
        
            Vector3 place = new Vector3(); // We allow the spawn position to increment for each loop
            place = spawnList[spawnToUse].transform.position; // Picks a random spawn point based on our check earlier to make sure it's unique

            GameObject newEnemy = (GameObject)Instantiate(enemyList[enemyRandomizer], place, Quaternion.identity); // This should take a value from the list of enemies and spawn it for each loop
            //newEnemy.transform.parent = transform; // Sets enemies as a child of enemy spawner
            newEnemy.name = newEnemy.name; // Does nothing but gives an example on how to pass vars to new spawned enemies

            GameManager.Instance.EnemyCount++; // Sends the data to the overall game manager as well
            if (GameManager.Instance.noEnemies == true) // Tells the game manager there are now enemies
                GameManager.Instance.noEnemies = false;

        }
    }

}
