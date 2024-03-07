using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace team03
{
    public class Knife : MicrogameEvents
    {
        bool ActionAllowed;
        public Transform fish;
        public bool CanDamage;

        public GameObject FishHitVFX;
        public GameObject Sushi;

        public AudioSource music;
        private void OnCollisionEnter(Collision collision)
        {
            if (ActionAllowed)
            {
                if (collision.transform.root == fish && CanDamage)
                {
                    Instantiate(FishHitVFX, fish.position, Quaternion.identity);
                    Instantiate(Sushi, fish.position, Quaternion.identity);
                    Destroy(fish.gameObject);
                    music.volume = Mathf.Clamp(music.volume, 0, music.volume -= Time.deltaTime);
                    ReportGameCompletedEarly();
                }
            }
        }

        protected override void OnTimesUp()
        {
            ActionAllowed = false;
            base.OnTimesUp();
        }

        protected override void OnGameStart()
        {
            ActionAllowed = true;
            base.OnGameStart();
        }
    }
}
