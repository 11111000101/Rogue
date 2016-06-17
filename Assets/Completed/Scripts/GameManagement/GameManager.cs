using UnityEngine;
using System.Collections;

namespace Completed
{
	public class GameManager
	{
		//**************** Singleton ***********************
        private static GameManager instance = null;

		private GameManager(){}
		
		public static GameManager getInstance()
		{
			if (GameManager.instance == null)
            {
				GameManager.instance = new GameManager();
                GameManager.getInstance().init();
            }
			return GameManager.instance;
		}

        private void init()
        {
            pData = new PlayerData();
        }
        //**************************************************
        private PlayerData pData = null;
        private LevelManager lvlManager = null;
        private int level = 1;
        //**************************************************

        public PlayerData getPlayerData()
        {
            return this.pData;
        }

        public void setLevelManager(LevelManager lvlManager)
        {
            this.lvlManager = lvlManager;
        }

        public LevelManager getLevelManager()
        {
            return this.lvlManager;
        }

        public int getLevel()
        {
            return this.level;
        }

        //**************************************************

        public void update()
        {

        }

        public void onClosePlayerInventory()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SceneMain");
        }

        public void onLevelFinished()
        {
            ++level;
            Object.Destroy(lvlManager);
            UnityEngine.SceneManagement.SceneManager.LoadScene("SceneInventory");
        }

        public void gameWon()
        {
            pData.onGameWon();
        }

        public void CheckForGameOver()
        {

        }

        //**************************************************


        ////Awake is always called before any Start functions
        //void Awake()
        //{
        //    //Check if instance already exists
        //    if (instance == null)
        //    {
        //        //if not, set instance to this
        //        instance = this;
        //    }
        //    //If instance already exists and it's not this:
        //    else if (instance != this)
        //    {
        //        //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
        //        Destroy(gameObject);
        //    }

        //    //Sets this to not be destroyed when reloading scene
        //    DontDestroyOnLoad(gameObject);
        //    //Get a component reference to the attached BoardManager script
        //    BoardManager boardScript = GetComponent<BoardManager>();
        //    this.pData = new PlayerData();
        //    if (instance.lvlManager == null)
        //        instance.lvlManager = gameObject.AddComponent<LevelManager>();
        //    instance.lvlManager.InitGame(boardScript, level);
        //}

        ////CheckIfGameOver checks if the player is out of hp and if so, ends the game.
        //public void CheckIfGameOver()
        //{
        //    //Check if hp total is less than or equal to zero.
        //    if (pData.getHP() <= 0)
        //    {
        //        //Call the PlaySingle function of SoundManager and pass it the gameOverSound as the audio clip to play.
        //        SoundManager.instance.PlaySingle(gameOverSound);
        //        //Stop the background music.
        //        SoundManager.instance.musicSource.Stop();
        //        //Call the GameOver function of GameManager.
        //        GameManager.instance.GameOver();
        //    }
        //}
    }
}

