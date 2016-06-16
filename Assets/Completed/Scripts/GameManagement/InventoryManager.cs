using UnityEngine;
using System.Collections;

namespace Completed
{
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager instance = null;             //Static instance of InventoryLoader which allows it to be accessed by any other script.

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
        }


        //This is called each time a scene is loaded.
        void OnLevelWasLoaded(int index)
        {
            print("OnLevelWasLoaded");
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
            print("closing");
            GameManager.getInstance().onClosePlayerInventory();
        }

        //private void loadGame()
        //{
        //    //Load the last scene loaded, in this case Main, the only scene in the game.
        //    //UnityEngine.SceneManagement.SceneManager.LoadScene("SceneMain");
        //    //Application.LoadLevel(Application.loadedLevel);
        //}
    }

}
