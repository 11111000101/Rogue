using UnityEngine;
using System;

namespace Completed
{
	//Enemy inherits from MovingObject, our base class for objects that can move, Player also inherits from this.
	public class Enemy : MovingObject, Character 
	{
		public AudioClip attackSound1;						//First of two audio clips to play when attacking the player.
		public AudioClip attackSound2;                      //Second of two audio clips to play when attacking the player.

        public int attackPower; 							//The amount of food points to subtract from the player when attacking.
        private float hp = 100;
        protected float defence = 0;
		
		private Animator animator;							//Variable of type Animator to store a reference to the enemy's Animator component.
		private Transform target;                           //Transform to attempt to move toward each turn.

        private const float Delay = 1f;
        private float nextMove;

        private Weapon weapon = new BasicSword();
		
		//Start overrides the virtual Start function of the base class.
		protected override void Start ()
		{
			//Register this enemy with our instance of GameManager by adding it to a list of Enemy objects. 
			//This allows the GameManager to issue movement commands.
			GameManager.getInstance().getLevelManager().AddEnemyToList (this);
			
			//Get and store a reference to the attached Animator component.
			animator = GetComponent<Animator> ();
			
			//Find the Player GameObject using it's tag and store a reference to its transform component.
			target = GameObject.FindGameObjectWithTag ("Player").transform;

            nextMove = Time.time + Delay;

            this.weapon.setParent(this.gameObject);

			//Call the start function of our base class MovingObject.
			base.Start ();
		}



        //The virtual keyword means AttemptMove can be overridden by inheriting classes using the override keyword.
        //AttemptMove takes a generic parameter T to specify the type of component we expect our unit to interact with if blocked (Player for Enemies, Wall for Player).
        protected virtual void AttemptMove<T>(int xDir, int yDir)
            where T : Component
        {
            if (Time.time < nextMove) return;
            nextMove = Time.time + Delay;

            //Hit will store whatever our linecast hits when Move is called.
            RaycastHit2D hit;

            //Set canMove to true if Move was successful, false if failed.
            bool canMove = Move(xDir, yDir, out hit);

            //Check if nothing was hit by linecast
            if (hit.transform == null)
                //If nothing was hit, return and don't execute further code.
                return;

            //Get a component reference to the component of type T attached to the object that was hit
            T hitComponent = hit.transform.GetComponent<T>();

            //If canMove is false and hitComponent is not equal to null, meaning MovingObject is blocked and has hit something it can interact with.
            if (!canMove && hitComponent != null)
            {
                nextMove = Time.time + weapon.getCastTime();
                Player hitPlayer = hitComponent as Player;
                animator.SetTrigger("enemyAttack");
                SoundManager.instance.RandomizeSfx(attackSound1, attackSound2);
                weapon.performAttack(transform.position, new Vector2(xDir, yDir));
            }
        }

        //MoveEnemy is called by the LevelManager each turn to tell each Enemy to try to move towards the player.
        public void MoveEnemy()
        {
            //Declare variables for X and Y axis move directions, these range from -1 to 1.
            //These values allow us to choose between the cardinal directions: up, down, left and right.
            int xDir = 0;
            int yDir = 0;

            //If the difference in positions is approximately zero (Epsilon) do the following:
            if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)

                //If the y coordinate of the target's (player) position is greater than the y coordinate of this enemy's position set y direction 1 (to move up). If not, set it to -1 (to move down).
                yDir = target.position.y > transform.position.y ? 1 : -1;

            //If the difference in positions is not approximately zero (Epsilon) do the following:
            else
                //Check if target x position is greater than enemy's x position, if so set x direction to 1 (move right), if not set to -1 (move left).
                xDir = target.position.x > transform.position.x ? 1 : -1;

            //Call the AttemptMove function and pass in the generic parameter Player, because Enemy is moving and expecting to potentially encounter a Player
            AttemptMove<Player>(xDir, yDir);
        }

        public float getHP()
        {
            return this.hp;
        }

        public void defend(IAttack attack)
        {
            this.hp -= Math.Max(0, attack.getAttackPower() - this.defence);
            if (this.hp > 0)
                print("Enemy lost " + (attack.getAttackPower() - this.defence) + " hp, new hp: " + this.hp);
            else
            {
                GameManager.getInstance().getLevelManager().RemoveEnemyFromList(this);
                Destroy(gameObject);
            }
        }
    }
}
