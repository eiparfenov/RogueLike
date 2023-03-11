using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource embient;
    [SerializeField] private AudioSource fight;
    [SerializeField] private AudioSource main;

    private void Start()
    {
        main.volume = 1;
        fight.volume = 0;
        embient.volume = 0.3f;
        
    }

    private void StartFight()
    {
        fight.volume = 0.3f;
    }
    private void StopFight()
    {
        fight.volume = 0;
    }
    
}
