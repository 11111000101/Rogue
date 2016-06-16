

namespace Completed
{
    using System.Collections;
    using System.Collections.Generic;       //Allows us to use Lists. 
    using UnityEngine;
    using UnityEngine.UI;					//Allows us to use UI.

    public class LevelManager : MonoBehaviour
    {
		public static LevelManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
		
        public float levelStartDelay = 2f;                      //Time to wait before starting level, in seconds.
        public float turnDelay = 0.1f;                          //Delay between each Player turn.

        private Text levelText;                                 //Text to display current level number.
        private GameObject levelImage;                          //Image to block out level as levels are being set up, background for levelText.
        private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
        private int level;                                      //Current level number, expressed in game as "Day 1".
        private List<Enemy> enemies;                            //List of all Enemy units, used to issue them move commands.
        private bool doingSetup = true;                         //Boolean to check if we're setting up board, prevent Player from moving during setup.

        //Awake is always called before any Start functions
        void Awake()
        {
            //Check if instance already exists
            if (instance == null)
                //if not, set instance to this
                instance = this;
            //If instance already exists and it's not this:
            else if (instance != this)
            {
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);
                //Sets this to not be destroyed when reloading scene
            }
            DontDestroyOnLoad(gameObject);
            //Assign enemies to a new List of Enemy objects.
            enemies = new List<Enemy>();
            //Get a component reference to the attached BoardManager script
            boardScript = GetComponent<BoardManager>();

            GameManager gManager = GameManager.getInstance();
            gManager.setLevelManager(this);
            this.level = gManager.getLevel();
            //Call the InitGame function to initialize the first level 
            InitGame();
            //Assign enemies to a new List of Enemy objects.
            enemies = new List<Enemy>();
        }


        //Initializes the game for each level.
        public void InitGame()
        {
            //While doingSetup is true the player can't move, prevent player from moving while title card is up.
            doingSetup = true;
            //Get a reference to our image LevelImage by finding it by name.
            levelImage = GameObject.Find("LevelImage");
            //Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
            levelText = GameObject.Find("LevelText").GetComponent<Text>();
            
            //Set the text of levelText to the string "Day" and append the current level number.
            levelText.text = "Day " + level;
            
            //Set levelImage to active blocking player's view of the game board during setup.
            levelImage.SetActive(true);
            
            //Call the HideLevelImage function with a delay in seconds of levelStartDelay.
            Invoke("HideLevelImage", levelStartDelay);
            
            //Clear any Enemy objects in our List to prepare for next level.
            enemies.Clear();
            
            //Call the SetupScene function of the BoardManager script, pass it current level number.
            boardScript.SetupScene(level);
        }
        
        //This is called each time a scene is loaded.
		void OnLevelWasLoaded(int index)
		{
			//Add one to our level number.
			level++;
			//Call InitGame to initialize our level.
			InitGame();
        }

        //Hides black image used between levels
		void HideLevelImage()
		{
			//Disable the levelImage gameObject.
			levelImage.SetActive(false);
			
			//Set doingSetup to false allowing player to move again.
			doingSetup = false;
        }

        //Update is called every frame.
		void Update()
		{
			//Check that doingSetup is not currently true.
			if(doingSetup)
				return;
			
			//Start moving enemies.
			StartCoroutine(MoveEnemies ());
		}

        //Coroutine to move enemies in sequence.
        IEnumerator MoveEnemies()
        {
            //Wait for turnDelay seconds, defaults to .1 (100 ms).
            yield return new WaitForSeconds(turnDelay);

            //If there are no enemies spawned (IE in first level):
            if (enemies.Count == 0)
            {
                //Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
                yield return new WaitForSeconds(turnDelay);
            }

            //Loop through List of Enemy objects.
            for (int i = 0; i < enemies.Count; i++)
            {
                //Call the MoveEnemy function of Enemy at index i in the enemies List.
                if (!enemies[i].isMoving())
                    enemies[i].MoveEnemy();

                //Wait for Enemy's moveTime before moving next Enemy, 
                yield return new WaitForSeconds(enemies[i].moveTime);
            }
        }

        //Call this to add the passed in Enemy to the List of Enemy objects.
        public void AddEnemyToList(Enemy script)
        {
            //Add Enemy to List enemies.
            enemies.Add(script);
        }
    }
}
