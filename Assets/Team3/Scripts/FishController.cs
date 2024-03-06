using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace team3
{
    public class FishController : MicrogameInputEvents
    {
        public bool button1Pressed;
        public Animator HeadAC;
        public Animator TailAC;
        Rigidbody rb;

        public bool LiftHead;
        public bool LiftTail;

        public float HorizontalForce;
        public float VerticalForce;
        public float GravityForce;

        public LayerMask groundLayer;
        public Transform groundCheck;
        public Vector3 groundCheckSize;
        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            TailAC.SetBool("LiftTail", LiftTail);
            HeadAC.SetBool("LiftHead", LiftHead);

            if (!isGrounded())
            {
                rb.AddForce(Vector3.down * GravityForce * rb.mass, ForceMode.Force);
            }
        }

        protected override void OnButton1Pressed(InputAction.CallbackContext context)
        {
            LiftHead = true;
        }

        protected override void OnButton1Released(InputAction.CallbackContext context)
        {
            LiftHead = false;
            Vector3 force = new Vector3(rb.velocity.x, VerticalForce, HorizontalForce);
            if (isGrounded())
            {
                rb.AddForce(force, ForceMode.Impulse);
            }
        }

        protected override void OnButton2Pressed(InputAction.CallbackContext context)
        {
            LiftTail = true;
        }

        protected override void OnButton2Released(InputAction.CallbackContext context)
        {
            LiftTail = false;
            Vector3 force = new Vector3(rb.velocity.x, VerticalForce, -HorizontalForce);
            if (isGrounded())
            {
                rb.AddForce(force, ForceMode.Impulse);
            }
        }

        bool isGrounded()
        {
            if(Physics.OverlapBox(groundCheck.position, groundCheckSize, Quaternion.identity, groundLayer).Length > 0)
            {
                return true;
            }
                return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
        }

    }
}

