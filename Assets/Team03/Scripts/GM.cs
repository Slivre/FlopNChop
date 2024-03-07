using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team03
{

    public class GM : MicrogameEvents
    {
        public GameObject Winner;
        public GameObject Chef;
        public GameObject Fish;

        public AudioSource music;
        protected override void OnTimesUp()
        {
            base.OnTimesUp();

            music.Stop();

            //if fish is dead
            if (Fish == null)
            {
                Winner = Chef;
            }
            else
            {
                Winner = Fish;
            }

            if(Winner == Fish)
            {
                //Do something
            }
            else if(Winner == Chef)
            {
                //Do something
            }
        }
    }
}

