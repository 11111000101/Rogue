using UnityEngine;
using System.Collections;
using System;

namespace Completed
{
    //Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
    public class Player : MovingObject
    {
        public float restartLevelDelay = 0.3f;        //Delay time in seconds to restart level.

        public int wallDamage = 1;                  //How much damage a player does to a wall when chopping it.
        public AudioClip moveSound1;                //1 of 2 Audio clips to play when player moves.
        public AudioClip moveSound2;                //2 of 2 Audio clips to play when player moves.
        public AudioClip eatSound1;                 //1 of 2 Audio clips to play when player collects a food object.
        public AudioClip eatSound2;                 //2 of 2 Audio clips to play when player collects a food object.
        public AudioClip drinkSound1;               //1 of 2 Audio clips to play when player collects a soda object.
        public AudioClip drinkSound2;               //2 of 2 Audio clips to play when player collects a soda object.

		private Animator animator;                  //Used to store a reference to the Player's animator component.

        //Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
        #if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
        private Vector2 touchOrigin = -Vector2.one; //Used to store location of screen touch origin for mobile controls.
        #endif

        //Start overrides the Start function of MovingObject
        protected override void Start ()
		{
			//Get a component reference to the Player's animator component
			animator = GetComponent<Animator>();

            //Get the current food point total stored in GameManager.instance between levels.
            //food = GameManager.instance.playerFoodPoints;

            //Set the foodText to reflect the current player food total.
            UserInterface.getInstance().updateAP();
            UserInterface.getInstance().updateHP();
			
			//Call the Start function of the MovingObject base class.
			base.Start ();
		}
		
		
		//This function is called when the behaviour becomes disabled or inactive.
		private void OnDisable ()
		{
			//When Player object is disabled, store the current local ap total in the GameManager so it can be re-loaded in next level.
			//GameManager.instance.playerAP = MAX_AP;
   //         GameManager.instance.playerHP = MAX_HP;
		}
		
		
		private void Update ()
		{
            //If it's not the player's turn, exit the function.
            //if(!GameManager.instance.playersTurn) return;

            getPlayerData().updateAP(Time.deltaTime);
			
			int horizontal = 0;  	//Used to store the horizontal move direction.
			int vertical = 0;		//Used to store the vertical move direction.
			
			//Check if we are running either in the Unity editor or in a standalone build.
			#if UNITY_STANDALONE || UNITY_WEBPLAYER
			
			//Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
			horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
			
			//Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
			vertical = (int) (Input.GetAxisRaw ("Vertical"));
			
			//Check if moving horizontally, if so set vertical to zero.
			if(horizontal != 0)
			{
				vertical = 0;
			}
			//Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
			#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
			
			//Check if Input has registered more than zero touches
			if (Input.touchCount > 0)
			{
				//Store the first touch detected.
				Touch myTouch = Input.touches[0];
				
				//Check if the phase of that touch equals Began
				if (myTouch.phase == TouchPhase.Began)
				{
					//If so, set touchOrigin to the position of that touch
					touchOrigin = myTouch.position;
				}
				
				//If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
				else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
				{
					//Set touchEnd to equal the position of this touch
					Vector2 touchEnd = myTouch.position;
					
					//Calculate the difference between the beginning and end of the touch on the x axis.
					float x = touchEnd.x - touchOrigin.x;
					
					//Calculate the difference between the beginning and end of the touch on the y axis.
					float y = touchEnd.y - touchOrigin.y;
					
					//Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
					touchOrigin.x = -1;
					
					//Check if the difference along the x axis is greater than the difference along the y axis.
					if (Mathf.Abs(x) > Mathf.Abs(y))
						//If x is greater than zero, set horizontal to 1, otherwise set it to -1
						horizontal = x > 0 ? 1 : -1;
					else
						//If y is greater than zero, set horizontal to 1, otherwise set it to -1
						vertical = y > 0 ? 1 : -1;
				}
			}
			
			#endif //End of mobile platform dependendent compilation section started above with #elif
			//Check if we have a non-zero value for horizontal or vertical
			if(horizontal != 0 || vertical != 0)
			{
				//Call AttemptMove passing in the generic parameter Wall, since that is what Player may interact with if they encounter one (by attacking it)
				//Pass in horizontal and vertical as parameters to specify the direction to move Player in.
				AttemptMove<Wall> (horizontal, vertical);
			}
		}
		
		//AttemptMove overrides the AttemptMove function in the base class MovingObject
		//AttemptMove takes a generic parameter T which for Player will be of the type Wall, it also takes integers for x and y direction to move in.
		protected override void AttemptMove <T> (int xDir, int yDir)
		{
            print("attemptMove");
            if (this.isMoving() || this.getPlayerData().hasEnoughAPToMove() == false)
            {
                return;
            }
            //Every time player moves, subtract from AP total.
            GameManager.instance.pData.playerMoved();

            //Update AP text display to reflect current score.
            UserInterface.getInstance().updateAP();
			
			//Call the AttemptMove method of the base class, passing in the component T (in this case Wall) and x and y direction to move.
			base.AttemptMove <T> (xDir, yDir);
			
			//Hit allows us to reference the result of the Linecast done in Move.
			RaycastHit2D hit;
			
			//If Move returns true, meaning Player was able to move into an empty space.
			if (Move (xDir, yDir, out hit)) 
			{
                print("move");
				//Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
				SoundManager.instance.RandomizeSfx (moveSound1, moveSound2);
			}
		}
		
		
		//OnCantMove overrides the abstract function OnCantMove in MovingObject.
		//It takes a generic parameter T which in the case of Player is a Wall which the player can attack and destroy.
		protected override void OnCantMove <T> (T component)
		{
			//Set hitWall to equal the component passed in as a parameter.
			Wall hitWall = component as Wall;
			
			//Call the DamageWall function of the Wall we are hitting.
			hitWall.DamageWall (wallDamage);
			
			//Set the attack trigger of the player's animation controller in order to play the player's attack animation.
			animator.SetTrigger ("playerChop");
		}
		
		
		//OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
		private void OnTriggerEnter2D (Collider2D other)
		{
			//Check if the tag of the trigger collided with is Exit.
			if(other.tag == "Exit")
			{
				//Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
				Invoke ("Restart", restartLevelDelay);
				
				//Disable the player object since level is over.
				enabled = false;
			}
			
			////Check if the tag of the trigger collided with is Food.
			//else if(other.tag == "Food")
			//{
			//	//Add pointsPerFood to the players current food total.
			//	food += pointsPerFood;
				
			//	//Update foodText to represent current total and notify player that they gained points
			//	foodText.text = "+" + pointsPerFood + " Food: " + food;
				
			//	//Call the RandomizeSfx function of SoundManager and pass in two eating sounds to choose between to play the eating sound effect.
			//	SoundManager.instance.RandomizeSfx (eatSound1, eatSound2);
				
			//	//Disable the food object the player collided with.
			//	other.gameObject.SetActive (false);
			//}

		}
		
		
		//Restart reloads the scene when called.
		private void Restart ()
		{
            //Load the last scene loaded, in this case Main, the only scene in the game.
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.sceneCount);
            //Application.LoadLevel(Application.loadedLevel);
		}

        public void showInventory()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Inventory");
        }

        public PlayerData getPlayerData()
        {
            return GameManager.instance.pData;
        }
	}
}

