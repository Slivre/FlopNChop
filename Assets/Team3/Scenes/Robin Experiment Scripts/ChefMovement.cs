using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace team03
{
    public class ChefMovement : MicrogameInputEvents
    {
        public bool HasPlayer;

        Rigidbody rb;

        public Transform fish;

        public float HorizontalForce;

        public float MoveDirection;

        private Vector3 dir = new Vector3(0, 0, 1);
        private Vector3 dir2 = new Vector3(0, 0, -1);

        //adjust this to change speed
        public float bobSpeed = 5f;
        //adjust this to change how high it goes
        public float height = 0.5f;

        public bool LPressed;
        public bool RPressed;

        //Code for chef timing

        public float chefCd;
        public bool chefStart = true;



        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            chefCd = Random.Range(3f, 5.0f);

        }

        private void Update()
        {
            if (HasPlayer)
            {
                MoveDirection = -stick.normalized.x;
                Move(MoveDirection);
            }
            else
            {
                Debug.Log(FollowFish());
                Move(FollowFish());
            }


            //get the objects current position and put it in a variable so we can access it later with less code
            Vector3 pos = transform.position;

            //calculate what the new Y position will be
            float newY = Mathf.Sin(Time.time * bobSpeed) * height + pos.y;
            //set the object's Y to the new calculated Y

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            //if (LPressed)
            //{
            //    transform.Translate(dir * HorizontalForce * Time.deltaTime);
            //}
            //if (RPressed)
            //{
            //    transform.Translate(dir2 * HorizontalForce * Time.deltaTime);
            //}
            //Timer For Chef Movement

            chefCd -= Time.deltaTime;

            //if (chefStart && chefCd < 0)
            //{
            //    if (fish.position.z < transform.position.z)
            //    {
            //        transform.Translate(dir2 * HorizontalForce * Time.deltaTime);
            //        Debug.Log("right");
            //    }

            //    else if (fish.position.z > transform.position.z)
            //    {
            //        transform.Translate(dir * HorizontalForce * Time.deltaTime);
            //        Debug.Log("left");
            //    }
            //    //else if (fish.position.z == transform.position.z)
            //    //{
            //    //    chefStart = false; // make sure we dont' call this again }


            //    //}
            //}
        }

        public void Move(float Dir)
        {           
            Vector3 HorizontalDir = new Vector3(Dir, 0, 0);
            Debug.Log(HorizontalDir);
            transform.Translate(HorizontalDir * HorizontalForce * Time.deltaTime);
        }

        public float FollowFish()
        {
            if (chefStart && chefCd <= 0)
            {
                if (fish.position.z < transform.position.z)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            else return 0;
        }

        protected override void OnButton1Pressed(InputAction.CallbackContext context)
        {
            LPressed = true;
            


            Debug.Log("Do action 1");

        }

        protected override void OnButton1Released(InputAction.CallbackContext context)
        {
            LPressed = false;
            Debug.Log("Stop action 1");

        }

        protected override void OnButton2Pressed(InputAction.CallbackContext context)
        {
            RPressed = true;
            Debug.Log("Do action 2");

        }

        protected override void OnButton2Released(InputAction.CallbackContext context)
        {
            RPressed = false;
            Debug.Log("Stop action 2");

        }
      
    }
} 

