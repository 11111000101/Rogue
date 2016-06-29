using UnityEngine;
using System;
using System.Collections.Generic; 		//Allows us to use Lists.
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

namespace Completed

{
    public class BoardManager : MonoBehaviour
    {
        // Using Serializable allows us to embed a class with sub properties in the inspector.
        [Serializable]
        public class Count
        {
            public int minimum;             //Minimum value for our Count class.
            public int maximum;             //Maximum value for our Count class.

            //Assignment constructor.
            public Count(int min, int max)
            {
                minimum = min;
                maximum = max;
            }
        }


        public int columns;                                         //Number of columns in our game board.
        public int rows;                                            //Number of rows in our game board.
        
        //public GameObject exit;                                         //Prefab to spawn for exit.
        public GameObject[] floorTiles;                                 //Array of floor prefabs.
        public GameObject[] enemyMeleeTiles;                            //Array of enemy prefabs.
        public GameObject[] enemyMagicTiles;                            //Array of enemy prefabs.
        public GameObject[] enemyRangedTiles;                            //Array of enemy prefabs.
        public GameObject[] wallTiles;                                  //Array of outer tile prefabs.

        private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
        private ILevel levelHolder;                                  //A variable to store a reference to the transform of our Level object.
        private List<Vector3> gridPositions = new List<Vector3>();  //A list of possible locations to place tiles.


        //Clears our list gridPositions and prepares it to generate a new board.
        void InitialiseList()
        {
            //Clear our list gridPositions.
            gridPositions.Clear();

            //Loop through x axis (columns).
            for (int x = 0; x < columns; x++)
            {
                //Within each column, loop through y axis (rows).
                for (int y = 0; y < rows; y++)
                {
                    //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                    gridPositions.Add(new Vector3(x, y, 0f));
                }
            }
        }


        //Sets up the walls and floor (background) of the game board.
        void BoardSetup()
        {
            //Instantiate Board and set boardHolder to its transform.
            boardHolder = new GameObject("Board").transform;

            //Loop along x axis, starting from -1 (to fill corner) with floor or wall edge tiles.
            for (int x = -1; x < columns + 1; x++)
            {
                //Loop along y axis, starting from -1 to place floor or wall tiles.
                for (int y = -1; y < rows + 1; y++)
                {
                    //Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                    //Check if we current position is at board edge, if so choose a random wall prefab from our array of wall tiles.
                    if (x == -1 || x == columns || y == -1 || y == rows)
                        toInstantiate = wallTiles[Random.Range(0, wallTiles.Length)];

                    //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                    GameObject instance =
                        Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                    instance.transform.SetParent(boardHolder);
                }
            }
        }

        //SetupScene initializes our level and calls the previous functions to lay out the game board
        public void SetupScene(int level)
        {
            levelHolder = new Level1();
            columns = levelHolder.getColumns();                                      //Number of columns in our game board.
            rows = levelHolder.getRows();                                            //Number of rows in our game board.

            //Creates the outer walls and floor.
            BoardSetup();

            //Reset our list of gridpositions.
            InitialiseList();

            Vector3[] wallPos = levelHolder.getWallPosition();
            Vector3[] meleePos = levelHolder.getEnemyMeleePosition();
            Vector3[] rangedPos = levelHolder.getEnemyRangedPosition();

            if (wallPos != null)
            {
                int a = wallPos.Length;
                for (int c = 0; c < a; c++)
                {
                    Instantiate(wallTiles[Random.Range(0, wallTiles.Length)], wallPos[c], Quaternion.identity);
                }
            }

            if (meleePos != null)
            {
                int a = meleePos.Length;
                for (int c = 0; c < a; c++)
                {
                    Instantiate(enemyMeleeTiles[0], meleePos[c], Quaternion.identity);
                }
            }

            if (rangedPos != null)
            {
                int a = rangedPos.Length;
                for (int c = 0; c < a; c++)
                {
                    Instantiate(enemyRangedTiles[0], rangedPos[c], Quaternion.identity);
                }
            }

            //Instantiate the exit tile in the upper right hand corner of our game board
            //Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
        }
    }
}
