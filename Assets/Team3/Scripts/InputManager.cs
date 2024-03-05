using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace team03

{

    public class InputManager : MicrogameInputEvents

    {

        public Vector2 direction;

        public bool button1Held;

        public float lastButton2Press;

        private void Update()
        {

            direction = stick.normalized; // (0, 0), (±1, 0), (0, ±1), (±0. 707, ±0.707)

            if (button1.IsPressed())
            {

                // Similar to Input.GetButton("Button 1") in the old system.

                button1Held = true;

            }
            else
            {

                button1Held = false;

            }

            if (button2.WasPressedThisFrame())
            {

                // Similar to Input.GetButtonDown("Button 2") in the old system.

                lastButton2Press = Time.time;

            }

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
    }
}
