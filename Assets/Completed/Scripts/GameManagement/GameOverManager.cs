using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {

    public Text WinLoseText { set; get; }
    public Text HealthPointsText { set; get; }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onWin(int MaxLives)
    {

    }

    public void GameOver()
    {
        ////Set levelText to display number of levels passed and game over message
        //levelText.text = "After " + level + " days, you died.";

        ////Enable black background image gameObject.
        //levelImage.SetActive(true);

        //Disable this GameManager.
        enabled = false;
    }

    void onRestartButtonClicked()
    {

    }

    void onExitButtonClicked()
    {

    }
}
