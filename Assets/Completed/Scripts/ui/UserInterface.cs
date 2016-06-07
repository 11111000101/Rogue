using UnityEngine;
using System.Collections;
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
            GameManager.instance.HealthPointsText.text = "HP: " + GameManager.instance.pData.getHP();
        }

        public void updateAP() {
            //if (ActionPointsText != null)
            GameManager.instance.ActionPointsText.text = "AP: " + (int)GameManager.instance.pData.getAP();
        }
    }

}