using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    [Serializable]
    public class PlayerSound
    {

        [SerializeField] private AudioClip[] steps;
        [SerializeField] private AudioClip[] stepsOnDirt;
        [SerializeField] private AudioClip getDamage;
        [SerializeField] private AudioClip blow;
        [SerializeField] private AudioClip coin;
        

        

        
        
        public void PlayStep(bool dirt,AudioSource component)
        {
            if (dirt)
            {
                var num = Random.Range(0, stepsOnDirt.Length);
                component.clip = stepsOnDirt[num];
                component.Play();
            }
            else
            {
                var num = Random.Range(0, steps.Length);
                component.clip = steps[num];
                component.Play();
            }
        }

        public void PlayGetDamage(AudioSource component)
        {
            component.clip = getDamage;
            component.Play();
        }

        public void PlayBlow(AudioSource component)
        {
            component.clip = blow;
            component.Play();
        }
        public void PlayCoin(AudioSource component)
        {
            component.clip = coin;
            component.Play();
        }
    }
}
