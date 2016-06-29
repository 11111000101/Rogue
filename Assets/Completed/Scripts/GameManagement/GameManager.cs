using UnityEngine;
using System;

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
        private InventoryManager invManager = null;
        private BoardManager boardScript = null;
        private LevelManager lvlManager = null;
        private GameOverManager gameOverManager = null;
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

        public void setGameOverManager(GameOverManager manager)
        {
            this.gameOverManager = manager;
        }

        public void setInventoryManager(InventoryManager invManager)
        {
            this.invManager = invManager;
        }

        public InventoryManager getInventoryManager()
        {
            return this.invManager;
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
            this.getLevelManager().Update();
        }

        public void onClosePlayerInventory()
        {
            UnityEngine.Object.Destroy(this.invManager);
            UnityEngine.SceneManagement.SceneManager.LoadScene("SceneMain");
        }

        public void onLevelFinished()
        {
            ++level;
            UnityEngine.Object.Destroy(this.boardScript);
            UnityEngine.Object.Destroy(this.lvlManager);
            this.getPlayerData().replenishAP();
            this.getPlayerData().replenishHP();
            UnityEngine.SceneManagement.SceneManager.LoadScene("SceneInventory");
        }

        public void gameWon()
        {
            pData.onGameWon();
        }

        public void CheckForGameOver()
        {
            if (pData.getHP() <= 0)
            {
                pData.lost();
                loadGameOverScene();
            }
        }

        public void setBoardScript(BoardManager boardScript)
        {
            this.boardScript = boardScript;
        }

        private void loadGameOverScene() // only in case of death...
        {
            UnityEngine.Object.Destroy(lvlManager);
            UnityEngine.SceneManagement.SceneManager.LoadScene("SceneGameOver");
            while (gameOverManager == null)
                continue;
            gameOverManager.GameOver();
        }

        public void save()
        {
            throw new NotImplementedException();
        }

        public void quit()
        {
            throw new NotImplementedException();
        }

        public void restart()
        {

        }
    }
}

