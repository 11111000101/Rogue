﻿using UnityEngine;
using System.Collections;

namespace Completed
{
    public class InventoryLoader : MonoBehaviour
    {
        public GameObject inventoryManager;

        void Awake()
        {
            //Check if a InventoryManager has already been assigned to static variable InventoryManager.instance or if it's still null
            if (InventoryManager.instance == null)

                //Instantiate gameManager prefab
                Instantiate(inventoryManager);

            ////Check if a SoundManager has already been assigned to static variable GameManager.instance or if it's still null
            //if (SoundManager.instance == null)

            //    //Instantiate SoundManager prefab
            //    Instantiate(soundManager);
        }
    }
}