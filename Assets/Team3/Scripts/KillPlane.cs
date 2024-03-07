using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team03
{
    public class KillPlane : MicrogameEvents
    {
        public Transform fish;
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.root == fish)
            {
                ReportGameCompletedEarly();
            }
        }
    }
}
