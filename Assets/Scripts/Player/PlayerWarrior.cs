using System;
using UnityEngine;

namespace Player
{
    public class PlayerWarrior : PlayerMovement
    {
        [SerializeField] private WarriorBlow Smash;

        [SerializeField] private float blowDuration;

        [SerializeField] private Transform handPivot;
    
    
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            base.OnCollisionEnter2D(collision);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }
        protected override void Special()
        {
            base.Special();
            var smash = Instantiate(Smash, transform.position,transform.rotation);
            smash.damage = playerStats.Damage;
            //Debug.Log(playerStats.SpecialItems.BulletSizeMult+"radus");
            smash.transform.localScale = Vector3.one*playerStats.SpecialItems.BulletSizeMult;
            
            smash.transform.eulerAngles = new Vector3(0, 0, GetAngleFromDirection());
            smash.transform.parent = transform;
            smash.blowDuration = blowDuration;
            if (Math.Abs(smash.transform.eulerAngles.z - 90) < 0.5)
            {
                smash.transform.position = smash.transform.position + new Vector3(0, 0, 1);
            }

        }

        protected override void NewDirection(Vector2 direction)
        {
            base.NewDirection(direction);
            handPivot.eulerAngles = new Vector3(0, 0, (transform.localScale.x>0?GetAngleFromDirection()+180 :(GetAngleFromDirection()==0?0:180-GetAngleFromDirection())));
            if (direction.y<0)
            {
                handPivot.transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
            }
            else
            {
                handPivot.transform.position = new Vector3(transform.position.x, transform.position.y, 0.5f);

            }
        }
    }
}
