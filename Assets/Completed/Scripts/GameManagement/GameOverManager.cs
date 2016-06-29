using UnityEngine;
using UnityEngine.UI;

namespace Completed
{
    public class GameOverManager : MonoBehaviour
    {
        public Text WinLoseText { set; get; }

        //Awake is always called before any Start functions
        void Awake()
        {
            GameManager.getInstance().setGameOverManager(this);
            if (GameManager.getInstance().getPlayerData().getMaxLife() <= 0)
            {
                GameObject.Find("ContinueBtn").SetActive(false);
            } else
            {
                GameObject.Find("RestartBtn").SetActive(false);
            }
            GameObject.Find("heartImg").GetComponentInChildren<Text>().text
                = "" + GameManager.getInstance().getPlayerData().getMaxLife();
        }

        // Use this for initialization
        void Start()
        {
        }

        public void setWinLoseText(string txt)
        {
            GameObject.Find("WinLose").GetComponent<Text>().text = txt;
        }

        public void setContinueBtnText(string txt)
        {
            GameObject.Find("ContinueButton").GetComponent<Button>().GetComponent<Text>().text = txt;
        }

        public void setCloseBtnText(string txt)
        {
            GameObject.Find("SaveAndExitButton").GetComponent<Button>().GetComponent<Text>().text = txt;
        }

        public void onWin(int MaxLives)
        {

        }

        public void GameOver()
        {
            string continueBtnText = "Restart";
            string winLoseText = "You lost!";
            string closeBtnText = "Close";
            if (GameManager.getInstance().getPlayerData().getCurrentNofLifes() > 0)
            {
                continueBtnText = "Continue";
                closeBtnText = "Save & Close";
            }
            setContinueBtnText(continueBtnText);
            setCloseBtnText(closeBtnText);
            setWinLoseText(winLoseText);
        }

        public void onContinueBtnClicked()
        {

        }

        public void onRestartButtonClicked()
        {

        }

        public void onExitButtonClicked()
        {
            //if lost, quit game without saving
            //GameManager manager = GameManager.getInstance();
            //manager.save();
            //manager.quit();
        }
    }
}
