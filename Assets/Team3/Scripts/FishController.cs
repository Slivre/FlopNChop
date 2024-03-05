using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace team3
{
    public class FishController : MicrogameInputEvents
    {
        public bool button1Pressed;
        Animator ac;
        Rigidbody rb;


        public bool LiftHead;
        public bool LiftTail;

        public float HorizontalForce;
        public float VerticalForce;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            ac = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (button1.IsPressed())
            {
                ac.SetBool("LiftHead", true);
                LiftHead = true;
            }
            else
            {
                LiftHead = false;
            }

            if (button2.IsPressed())
            {
                ac.SetBool("LiftTail", true);
                LiftTail = true;
            }
            else
            {
                LiftTail = false;
            }

            

        }

        protected override void OnButton1Pressed(InputAction.CallbackContext context)
        {
            LiftHead = true;
            Debug.Log("Do action 1");

        }

        protected override void OnButton1Released(InputAction.CallbackContext context)
        {
            LiftHead = false;
            Vector3 force = new Vector3(rb.velocity.x, VerticalForce, HorizontalForce);
            rb.AddForce(force, ForceMode.Impulse);
            Debug.Log("Stop action 1");

        }

        protected override void OnButton2Pressed(InputAction.CallbackContext context)
        {
            LiftTail = true;
            Debug.Log("Do action 2");

        }

        protected override void OnButton2Released(InputAction.CallbackContext context)
        {
            LiftTail = false;
            Vector3 force = new Vector3(rb.velocity.x, VerticalForce, -HorizontalForce);
            rb.AddForce(force, ForceMode.Impulse);
            Debug.Log("Stop action 2");

        }
    }
}

