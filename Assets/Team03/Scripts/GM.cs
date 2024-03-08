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
        public static bool gameEnd;

        private void Start()
        {
            gameEnd = false;
        }

        private void Update()
        {
            if (gameEnd)
            {
                MusicFadeOut();
            }
        }

        protected override void OnTimesUp()
        {
            music.volume = Mathf.Clamp(music.volume,0, music.volume -= Time.deltaTime);
            gameEnd = true;
            base.OnTimesUp();

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

        public void MusicFadeOut()
        {
            music.volume = Mathf.Clamp(music.volume, 0, music.volume -= Time.deltaTime);
        }
    }
}

