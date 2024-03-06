using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace team03
{
    public class ChefMovement : MicrogameInputEvents
    {
        public bool HasPlayer;

        public float FollowDelay;
        float currentFollowDelay;
        float fishDirPreDelay;

        public Animator ArmAC;
        public Animator HeadAC;

        public Transform fish;

        public Vector3 knifeOffset;

        public bool FishInRange;

        public float HorizontalForce;
        public float MoveDirection;

        //adjust this to change speed
        public float bobSpeed = 5f;
        //adjust this to change how high it goes
        public float height = 0.5f;

        //Code for chef timing
        public float chefCd;
        public bool chefStart = true;

        private void Start()
        {
            chefCd = Random.Range(3f, 5.0f);
        }      

        private void Update()
        {
           
            //Updates the fish's direction every few seconds
            if (currentFollowDelay < FollowDelay)
            {
                currentFollowDelay += Time.deltaTime;
            }
            else
            {
                fishDirPreDelay = FollowFish();
                currentFollowDelay = 0;
            }

            //Move the chef based on have player or not
            if (HasPlayer)
            {
                MoveDirection = -stick.normalized.x;
                Move(MoveDirection);
            }
            else
            {
                if (!FishInRange)
                {
                    Move(fishDirPreDelay);
                }
            }

            //get the objects current position and put it in a variable so we can access it later with less code
            Vector3 pos = transform.position;

            //calculate what the new Y position will be
            float newY = Mathf.Sin(Time.time * bobSpeed) * height + pos.y;
            //set the object's Y to the new calculated Y

            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            //Timer For Chef Movement

            chefCd -= Time.deltaTime;
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
                if (fish.position.z < transform.position.z + knifeOffset.z)
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
            Debug.Log("Do action 1");

        }

        protected override void OnButton1Released(InputAction.CallbackContext context)
        {
            Debug.Log("Stop action 1");

        }

        protected override void OnButton2Pressed(InputAction.CallbackContext context)
        {
            Debug.Log("Do action 2");

        }

        protected override void OnButton2Released(InputAction.CallbackContext context)
        {
            Debug.Log("Stop action 2");

        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.transform == fish)
            {
                FishInRange = true;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position + knifeOffset, 1);
        }
    }
} 

