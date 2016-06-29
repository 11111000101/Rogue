using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace Completed
{
    //Player inherits from MovingObject, our base class for objects that can move, Enemy also inherits from this.
    public class Player : MovingObject, Character
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

        private int up = 0;
        private int right = 1;
        
        private float nextAction;

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

            addDirectionArrow();
            changeDirection();

            nextAction = Time.time;
			
			//Call the Start function of the MovingObject base class.
			base.Start ();
		}

        GameObject directionArrow = null;

        private void addDirectionArrow()
        {
            directionArrow = new GameObject();
            Texture2D tex = RogueUtils.getTextureFromPath(RogueUtils.getUIIconDir()
                + "/directionArrow.png");
            directionArrow.AddComponent<SpriteRenderer>().sprite = RogueUtils.generateSpriteFromTexture(tex);
            directionArrow.layer = LayerMask.NameToLayer("UI");
            directionArrow.GetComponent<SpriteRenderer>().sortingLayerName = "Units";
            directionArrow.name = "PlayerDirection";
            directionArrow.transform.position = new Vector3(0.45f, 0);
            directionArrow.transform.SetParent(transform);
        }
		
        private void changeDirection()
        {
            Transform tr = directionArrow.transform;
            Vector3 pos = gameObject.transform.position;
            if (up != 0)
            {
                tr.position = new Vector3(pos.x, 0.45f * up + pos.y);
                tr.rotation = Quaternion.Euler(0, 0, 90f * up);
            }
            else if (right != 0)
            {
                tr.position = new Vector3(0.45f * right + pos.x, pos.y);
                tr.rotation = new Quaternion(0, 0, (right == 1?0:180), 0);
            }
        }
		
		//This function is called when the behaviour becomes disabled or inactive.
		private void OnDisable ()
		{
			//When Player object is disabled, store the current local ap total in the GameManager so it can be re-loaded in next level.
			//GameManager.instance.playerAP = MAX_AP;
   //         GameManager.instance.playerHP = MAX_HP;
		}
		
        public void checkForMovement()
        {
            int horizontal = 0;     //Used to store the horizontal move direction.
            int vertical = 0;       //Used to store the vertical move direction.

            //Check if we are running either in the Unity editor or in a standalone build.
#if UNITY_STANDALONE || UNITY_WEBPLAYER

            //Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
            horizontal = (int)(Input.GetAxisRaw("Horizontal"));

            //Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
            vertical = (int)(Input.GetAxisRaw("Vertical"));

            //Check if moving horizontally, if so set vertical to zero.
            if (horizontal != 0)
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

            int facingDirectionHorizontal = (int)(Input.GetAxisRaw("viewHorizontal"));
            int facingDirectionVertical = (int)(Input.GetAxisRaw("viewVertical"));

            //Check if viewing horizontally, if so set vertical to zero.
            if (facingDirectionHorizontal != 0)
            {
                facingDirectionVertical = 0;
            }

            //Check if we have a non-zero value for horizontal or vertical
            if (facingDirectionHorizontal != 0 || facingDirectionVertical != 0)
            {
                up = facingDirectionVertical;
                right = facingDirectionHorizontal;
                changeDirection();
            }

            //Check if we have a non-zero value for horizontal or vertical
            if (horizontal != 0 || vertical != 0)
            {
                up = vertical;
                right = horizontal;

                changeDirection();

                if (this.isMoving() || this.getPlayerData().hasEnoughAPToMove() == false || Time.time < nextAction)
                {
                    return;
                }

                nextAction = Time.time + moveTime;

                //Every time player moves, subtract from AP total.
                GameManager.getInstance().getPlayerData().playerMoved();

                //Hit allows us to reference the result of the Linecast done in Move.
                RaycastHit2D hit;

                //If Move returns true, meaning Player was able to move into an empty space.
                if (Move(horizontal, vertical, out hit))
                {
                    //Call RandomizeSfx of SoundManager to play the move sound, passing in two audio clips to choose from.
                    SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
                }
            }
        }
		
		private void Update ()
		{
            getPlayerData().updateAP(Time.deltaTime);
            if (nextAction > Time.time)
                return;
            if (Input.GetButton("Attack"))
            {
                if (getPlayerData().getMainWeapon().getRequiredActionPoints() < getPlayerData().getAP())
                {
                    { }
                    float castTime = getPlayerData().getMainWeapon().getCastTime();
                    float animationTime = 0.1f;
                    nextAction = Time.time + castTime + animationTime;
                    castMainAttack();
                }
            }
            else
            {
                checkForMovement();
            }
		}

        private void castMainAttack()
        {
            Weapon weapon = getPlayerData().getMainWeapon();
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            weapon.performAttack(new Vector2(player.position.x, player.position.y),
                                 new Vector2(right, up));
        }

		//OnTriggerEnter2D is sent when another object enters a trigger collider attached to this object (2D physics only).
		private void OnTriggerEnter2D (Collider2D other)
		{
			//Check if the tag of the trigger collided with is Exit.
			if(other.tag == "Exit")
            {
                //Disable the player object since level is over.
                enabled = false;
                GameManager.getInstance().onLevelFinished();
				//Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
				//Invoke ("Restart", restartLevelDelay);
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
            return GameManager.getInstance().getPlayerData();
        }

        public void defend(IAttack attack)
        {
            this.getPlayerData().defend(attack);
        }

        public float getHP()
        {
            return this.getPlayerData().getHP();
        }
    }
}

