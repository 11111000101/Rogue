using UnityEngine;
using UnityEngine.UI;	//Allows us to use UI.

namespace Completed {
    public class UserInterface {

        private volatile static UserInterface instance = null;

        private UserInterface() { }

        public static UserInterface getInstance() {
            if (UserInterface.instance == null)
            {
                UserInterface.instance = new UserInterface();
            }
            return UserInterface.instance;
        }

        public void updateHP() {
            //if (HealthPointsText != null)
            GameObject.Find("HealthPointsText").GetComponent<Text>().text = "HP: " + GameManager.getInstance().getPlayerData().getHP();
            GameObject.Find("HPImage").GetComponent<Image>().fillAmount =
                GameManager.getInstance().getPlayerData().getHP()
                / PlayerData.MaxHP;
        }

        public void updateAP() {
            GameObject.Find("ActionPointsText").GetComponent<Text>().text = "AP: " + (int)GameManager.getInstance().getPlayerData().getAP();
            GameObject.Find("APImage").GetComponent<Image>().fillAmount =
                GameManager.getInstance().getPlayerData().getAP()
                / PlayerData.MaxAP;
        }
    }

}