using System;
using UnityEngine;

namespace Completed
{
    public class BasicProjectile : MonoBehaviour, IAttack {
        private LongRangeWeapon weapon;
        private Vector2 direction;
        private GameObject parent;
        private int resultingPower;

        void Awake()
        {
            gameObject.name = "BasicProjectile";
            //gameObject.transform.SetParent(GameObject.FindObjectOfType<Canvas>().transform);
        }

        void Start()
        {
            gameObject.GetComponent<Rigidbody2D>().mass = 1;

        }

        void Update() {
            gameObject.GetComponent<Rigidbody2D>().AddForce(direction, ForceMode2D.Impulse);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weapon.getRange(), 
                1 << LayerMask.NameToLayer("Units") |
                1 << LayerMask.NameToLayer("Wall")  |
                1 << LayerMask.NameToLayer("BlockingLayer"));
            if (hit.transform != null)
            {
                Character hitComponent = hit.transform.GetComponent<Character>();
                if (hitComponent != null) // is Character
                {
                    hitComponent.defend(this);
                }
                OnCollisionEnter();
            }
        }

        public void setWeapon(LongRangeWeapon weapon)
        {
            this.weapon = weapon;
            this.resultingPower = weapon.getAttackPower();
        }
        

        public void setDirection(Vector2 direction)
        {
            this.direction = direction;
            if (direction.x == -1)
                gameObject.transform.Rotate(0, 0, 180);
            else if (direction.y == 1)
                gameObject.transform.Rotate(0, 0, 90);
            else if (direction.y == -1)
                gameObject.transform.Rotate(0, 0, -90);
        }

        void OnCollisionEnter()
        {
            print("Hit ");
            Destroy(gameObject);
        }

        public void setParent(GameObject parent)
        {
            this.parent = parent;
        }

        public int getAttackPower()
        {
            return weapon.getAttackPower();
        }

        public IElement getElement()
        {
            return weapon.getElement();
        }

        public void animate(Vector2 pos, Vector2 direction)
        {
            return;
        }

        public float checkProjection(Vector2 from, Vector2 directionFacing)
        {
            return 0f;
        }

        public void setResultingPower(int v)
        {
            this.resultingPower = v;
        }
    }
}