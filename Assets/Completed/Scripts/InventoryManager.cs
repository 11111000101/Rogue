using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour {
    public static InventoryManager instance = null;				//Static instance of InventoryLoader which allows it to be accessed by any other script.
    
    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        ////Assign enemies to a new List of Enemy objects.
        //enemies = new List<Enemy>();

        ////Get a component reference to the attached BoardManager script
        //boardScript = GetComponent<BoardManager>();

        //this.pData = new PlayerData();


        ////Call the InitGame function to initialize the first level 
        //InitGame();
    }


    //This is called each time a scene is loaded.
    void OnLevelWasLoaded(int index)
    {
        ////Add one to our level number.
        //level++;
        ////Call InitGame to initialize our level.
        //InitGame();
    }

    //Initializes the game for each level.
    void InitGame()
    {
        ////While doingSetup is true the player can't move, prevent player from moving while title card is up.
        //doingSetup = true;

        ////Get a reference to our image LevelImage by finding it by name.
        //levelImage = GameObject.Find("LevelImage");

        ////Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
        //levelText = GameObject.Find("LevelText").GetComponent<Text>();

        ////Set the text of levelText to the string "Day" and append the current level number.
        //levelText.text = "Day " + level;


        //ActionPointsText = GameObject.Find("ActionPointsText").GetComponent<Text>();
        //HealthPointsText = GameObject.Find("HealthPointsText").GetComponent<Text>();

        ////Set levelImage to active blocking player's view of the game board during setup.
        //levelImage.SetActive(true);

        ////Call the HideLevelImage function with a delay in seconds of levelStartDelay.
        //Invoke("HideLevelImage", levelStartDelay);

        ////Clear any Enemy objects in our List to prepare for next level.
        //enemies.Clear();

        ////Call the SetupScene function of the BoardManager script, pass it current level number.
        //boardScript.SetupScene(level);
    }

    //Update is called every frame.
    void Update()
    {
        ////Check that doingSetup is not currently true.
        //if (doingSetup)
        //    //If doingSetup is true, return and do not start MoveEnemies.
        //    return;

        ////Start moving enemies.
        //StartCoroutine(MoveEnemies());
    }


    public void onExitBtnClicked()
    {
        print("exit");
    }
}
