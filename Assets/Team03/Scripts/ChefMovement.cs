using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

namespace team03
{
    public class ChefMovement : MicrogameInputEvents
    {
        bool ActionAllowed;
        public bool HasPlayer;

        public float FollowDelay;
        float currentFollowDelay;
        float fishDirPreDelay;

        public Animator ArmAC;
        public Animator TableAC;

        public Transform fish;
        public Transform Head;
        public Knife knife;

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

        public AudioSource ChefAudioSource;
        public AudioSource KnifeAudioSource;

        public AudioClip[] ChefSFXClips;
        private void Start()
        {
            chefCd = Random.Range(3f, 5.0f);
        }      

        private void Update()
        {
            Head.LookAt(fish);

            if (ActionAllowed)
            {
                //Updates the fish's direction every few seconds
                if (currentFollowDelay < FollowDelay)
                {
                    currentFollowDelay += Time.deltaTime;
                }
                else
                {
                    if (fish != null)
                    {
                        fishDirPreDelay = FollowFish();
                        currentFollowDelay = 0;
                    }
                }

                //Move the chef based on have player or not
                //Stop the player moving if they are chopping
                if (HasPlayer && !FishInRange)
                {
                    MoveDirection = -stick.normalized.x;
                    Move(MoveDirection);
                }
                else
                {
                    if (fish != null)
                    {
                        if (!FishInRange)
                        {
                            Move(fishDirPreDelay);
                        }
                    }
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
            Chop();
        }

        protected override void OnButton1Released(InputAction.CallbackContext context)
        {
        }

        protected override void OnButton2Pressed(InputAction.CallbackContext context)
        {
        }

        protected override void OnButton2Released(InputAction.CallbackContext context)
        {
        }

        //Ai chef chops the fish when it in in range of collider
        private void OnTriggerEnter(Collider other)
        {
            if(other.transform == fish && !HasPlayer)
            {
                float delay = Random.Range(0f,1f);
                Invoke("Chop", delay);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position + knifeOffset, 1);
        }

        //tell the animator to play chop animation
        public void Chop()
        {
            if (!FishInRange)
            {
                ChefAudioSource.clip = ChefSFXClips[Random.Range(0, ChefSFXClips.Length)];
                ChefAudioSource.Play();
            }
            FishInRange = true;
            ArmAC.SetBool("Chop",true);
        }

        //Set the knife to able to deal damage
        public void canDamage()
        {
            knife.CanDamage = true;           
        }

        //Set the knife to unable to deal damage
        public void EndDamage()
        {
            knife.CanDamage = false;
        }

        //End the chop
        public void EndChop()
        {
            FishInRange = false;
            ArmAC.SetBool("Chop", false);
        }

        //Shake the objects on table
        public void ObjectShake()
        {
            TableAC.Play("TableObjectsBounce");
        }

        //Turn off actions when times up
        protected override void OnTimesUp()
        {
            base.OnTimesUp();
            ActionAllowed = false;
        }

        protected override void OnGameStart()
        {
            ActionAllowed = true;
            base.OnGameStart();
        }

        public void PlayChopSound()
        {
            KnifeAudioSource.Play();
        }
    }
} 

