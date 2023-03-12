using System;
using System.Collections.Generic;
using System.Linq;
using Items;
using Signals;
using UnityEngine;
using Utils.Signals;

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
        [SerializeField] private int startCoins;
        public Special SpecialItems { get; private set; } = new();
        
        private int maxHealth;
        private int health;
        private float invincibleTime;
        private float speed;
        private float damage;
        private float attackSpeed;
        private float reclining;
        private int coins;
        
        private List<BaseItem> _items = new List<BaseItem>();
        public int Health { get => health; set => health = value; }
        public int MaxHealth => maxHealth;
        public float InvincibleTime => invincibleTime;
        public float Speed => speed;
        public float Damage => damage;
        public float AttackSpeed => attackSpeed;
        public float Reclining => reclining;

        public int Coins
        {
            get => coins;
            set
            {
                coins = value;
                SignalBus.Invoke(new PlayerCoinsChangedSignal(){CurrentCoins = coins});
            }
        }

        public void AddItem(BaseItem simpleItem)
        {
            if (simpleItem is HeartItem heartItem)
            {
                health += heartItem.Heal;
                health = Mathf.Clamp(health, 0, maxHealth);
            }
            else if (simpleItem is CoinItem coinItem)
            {
                Coins += coinItem.Cash;
            }
            else
            {
                _items.Add(simpleItem);
                RecalculateStats();
            }
        }

        public void Init()
        {
            Coins = startCoins;
        }
        public void RecalculateStats()
        {
            damage = startDamage;
            maxHealth = startMaxHealth;
            invincibleTime = startInvincibleTime;
            speed = startSpeed;
            attackSpeed = startAttackSpeed;
            reclining = startReclining;


            foreach (var item in _items)
            {
                item.ChangeDamage(ref damage);
                item.ChangeReclining(ref reclining);
                item.ChangeAttackRate(ref attackSpeed);
                item.ChangeSpeed(ref speed);
                item.ChangeInvincibleTime(ref invincibleTime);
                item.ChangeMaxHealth(ref maxHealth);
            }

            SpecialItems.Refresh(_items);
        }

        public class Special
        {
            private float vampirismChance;
            private float greedChance;
            private float luckyChance;
            private float bulletSizeMult;
            private float powerMult;

            private bool rotHit;
            private bool doubleHit;
            private float radiusHit;

            public float VampirismChance => vampirismChance;
            public float GreedChance => greedChance;
            public float LuckyChance => luckyChance;
            public float BulletSizeMult => bulletSizeMult;
            public float PowerMult => powerMult;
            public bool RotHit => rotHit;
            public bool DoubleHit => doubleHit;
            public float RadiusHit => radiusHit;

            public void Refresh(List<BaseItem> items)
            {
                var specialItems = items.OfType<SpecialItem>().ToArray();
                
                vampirismChance = specialItems.Where(x => x.Type == SpecialItem.SpecialItemType.Vampirism).Select(x => x.Chance).Sum();
                greedChance = specialItems.Where(x => x.Type == SpecialItem.SpecialItemType.Greed).Select(x => x.Chance).Sum();
                luckyChance = specialItems.Where(x => x.Type == SpecialItem.SpecialItemType.LuckyChance).Select(x => x.Chance).Sum();
                var bulletSizeItems = specialItems.Where(x => x.Type == SpecialItem.SpecialItemType.MoreSize).ToArray();
                if (bulletSizeItems.Length == 0)
                {
                    bulletSizeMult = 1;
                }
                else if (bulletSizeItems.Length == 1)
                {
                    bulletSizeMult = bulletSizeItems[0].Chance;
                }
                else
                {
                    bulletSizeMult = specialItems.Where(x => x.Type == SpecialItem.SpecialItemType.MoreSize).Select(x => x.Chance).Aggregate((x, y) => x * y);
                }
                //powerMult = specialItems.Where(x => x.Type == SpecialItem.SpecialItemType.PowerHit).Select(x => x.Chance).Aggregate((x, y) => x * y);

                rotHit = specialItems.Any(x => x.Type == SpecialItem.SpecialItemType.AttackOnRotation);
                doubleHit = specialItems.Any(x => x.Type == SpecialItem.SpecialItemType.MoreHit);

                radiusHit = specialItems.Where(x => x.Type == SpecialItem.SpecialItemType.RadiusHits).Select(x => x.Radius).Sum();
            }
            
        }

        public class ReloadingItems
        {
            
        }
    }
}