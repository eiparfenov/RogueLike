using System;
using System.Collections;
using System.Collections.Generic;
using Signals;
using Utils.Signals;
using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource embient;
    [SerializeField] private AudioSource fight;
    [SerializeField] private AudioSource main;

    private void Start()
    {
        main.volume = 0.5f;
        fight.volume = 0;
        embient.volume = 0.3f;
        SignalBus.AddListener<FightSignal>(SetFight);
        
    }

    private void SetFight(FightSignal signal)
    {
        if (signal.InProgress)
        {
            fight.volume = 0.8f;
            main.volume = 0.3f;
            embient.volume = 0.5f;
        }
        else
        {
            main.volume = 0.6f;
            fight.volume = 0; 
            embient.volume = 0.3f;
        }
    }

    private void OnDestroy()
    {
        SignalBus.RemoveListener<FightSignal>(SetFight);
    }
}
