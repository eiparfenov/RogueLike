using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [Serializable]
    public class EnemyStats 
    {
        [SerializeField] private int damage;
        [SerializeField] private float health;
        [SerializeField] private float speed;
        [SerializeField] private float stunTime;
        [SerializeField] private float recliningValue;
    
        public float Health { get => health; set => health = value; }
        public int Damage { get => damage; set => damage = value; }
        public float Speed { get => speed; set => speed = value; }
        public float StunTime { get => stunTime; set => stunTime = value; }
        public float RecliningValue { get => recliningValue; set => recliningValue = value; }
    }
}

