using System;
using NaughtyAttributes;
using UnityEngine;

namespace Items
{
    public abstract class BaseItem: ScriptableObject
    {
        [field: SerializeField]
        [field: Required("Вы забыли установить картинку.")]
        public Sprite Image { get; private set; } 
        
        [field: SerializeField]
        public String Description { get; private set; } 
        public abstract bool FallowPlayer { get; }

        public bool wasDropped;
        #region ChangeStats
        [field: SerializeField] public bool ChangesBaseStats { get; private set; }
        
        [field: Foldout("Damage")]
        [field: ShowIf(nameof(ChangesBaseStats))]
        [field: SerializeField] 
        public int DamageAdditional { get; private set; } = 0;
        [field: Foldout("Damage")]
        [field: ShowIf(nameof(ChangesBaseStats))]
        [field: SerializeField] 
        public float DamageMultiplier { get; private set; } = 1;
        
        [field: Foldout("InvincibleTime")]
        [field: ShowIf(nameof(ChangesBaseStats))]
        [field: SerializeField] 
        public int InvincibleTimeAdditional { get; private set; } = 0;
        
        [field: Foldout("InvincibleTime")]
        [field: ShowIf(nameof(ChangesBaseStats))]
        [field: SerializeField] 
        public float InvincibleTimeMultiplier { get; private set; } = 1;
        [field: Foldout("Speed")]
        [field: ShowIf(nameof(ChangesBaseStats))]
        [field: SerializeField] 
        public float SpeedAdditional { get; private set; } = 0;
        
        [field: Foldout("Speed")]
        [field: ShowIf(nameof(ChangesBaseStats))]
        [field: SerializeField] 
        public float SpeedMultiplier { get; private set; } = 1;
        
        [field: Foldout("MaxHealth")]
        [field: ShowIf(nameof(ChangesBaseStats))]
        [field: SerializeField] 
        public int MaxHealthAdditional { get; private set; } = 0;
        
        [field: Foldout("MaxHealth")]
        [field: ShowIf(nameof(ChangesBaseStats))]
        [field: SerializeField] 
        public float MaxHealthMultiplier { get; private set; } = 1;
        
        [field: Foldout("AttackRate")]
        [field: ShowIf(nameof(ChangesBaseStats))]
        [field: SerializeField] 
        public int AttackRateAdditional { get; private set; } = 0;
        
        [field: Foldout("AttackRate")]
        [field: ShowIf(nameof(ChangesBaseStats))]
        [field: SerializeField] 
        public float AttackRateMultiplier { get; private set; } = 1;
        
        [field: Foldout("Reclining")]
        [field: ShowIf(nameof(ChangesBaseStats))]
        [field: SerializeField] 
        public int RecliningAdditional { get; private set; } = 0;
        
        [field: Foldout("Reclining")]
        [field: ShowIf(nameof(ChangesBaseStats))]
        [field: SerializeField] 
        public float RecliningMultiplier { get; private set; } = 1;

        public virtual void ChangeSpeed(ref float speed)
        {
            speed *= SpeedMultiplier;
            speed += SpeedAdditional;
        }

        public virtual void ChangeDamage(ref int damage)
        {
            damage = (int) (damage * DamageMultiplier);
            damage += DamageAdditional;
        }

        public virtual void ChangeMaxHealth(ref int maxHealth)
        {
            maxHealth = (int) (maxHealth * MaxHealthMultiplier);
            maxHealth += MaxHealthAdditional;
        }

        public virtual void ChangeAttackRate(ref float attackRate)
        {
            attackRate *= AttackRateMultiplier;
            attackRate += AttackRateAdditional;
        }

        public virtual void ChangeReclining(ref float reclining)
        {
            reclining *= RecliningMultiplier;
            reclining += RecliningAdditional;
        }

        public virtual void ChangeInvincibleTime(ref float invincibleTime)
        {
            invincibleTime *= InvincibleTimeMultiplier;
            invincibleTime += InvincibleTimeAdditional;
        }
        #endregion
    }
}