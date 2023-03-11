using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerStats
    {
        [SerializeField] private int startMaxHealth;
        [SerializeField] private int startHealth;
        [SerializeField] private float startInvincibleTime;
        [SerializeField] private float startSpeed;
        [SerializeField] private int startDamage;
        [SerializeField] private float startAttackSpeed;
        [SerializeField] private float startReclining;
        
        private int maxHealth;
        private int health;
        private float invincibleTime;
        private float speed;
        private int damage;
        private float attackSpeed;
        private float reclining;
        
        private List<BaseItem> _simpleItems = new List<BaseItem>();
        public int Health { get => health; set => health = value; }
        public int MaxHealth => maxHealth;
        public float InvincibleTime => invincibleTime;
        public float Speed => speed;
        public int Damage => damage;
        public float AttackSpeed => attackSpeed;
        public float Reclining => reclining;

        public void AddItem(BaseItem simpleItem)
        {
            if (simpleItem is HeartItem heartItem)
            {
                health += heartItem.Heal;
                health = Mathf.Clamp(health, 0, maxHealth);
            }
            else
            {
                _simpleItems.Add(simpleItem);
                RecalculateStats();
            }
        }

        public void RecalculateStats()
        {
            damage = startDamage;
            health = startHealth;
            maxHealth = startMaxHealth;
            invincibleTime = startInvincibleTime;
            speed = startSpeed;
            attackSpeed = startAttackSpeed;
            reclining = startReclining;

            foreach (var item in _simpleItems)
            {
                item.ChangeDamage(ref damage);
                item.ChangeReclining(ref reclining);
                item.ChangeAttackRate(ref attackSpeed);
                item.ChangeSpeed(ref speed);
                item.ChangeInvincibleTime(ref invincibleTime);
                item.ChangeMaxHealth(ref maxHealth);
            }
        }
    }
}