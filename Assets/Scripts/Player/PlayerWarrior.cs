using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWarrior : PlayerMovement
{
    [SerializeField] private GameObject Smash;

    [SerializeField] private float blowDuration;

    
    
    
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
            smash.transform.eulerAngles = new Vector3(0, 0, GetAngleFromDirection());
            smash.transform.parent = transform;
            smash.GetComponent<WarriorBlow>().blowDuration = blowDuration;
            if (Math.Abs(smash.transform.eulerAngles.z - 90) < 0.5)
            {
                smash.transform.position = smash.transform.position + new Vector3(0, 0, 1);
            }
            
        
}

    
    
    
}
