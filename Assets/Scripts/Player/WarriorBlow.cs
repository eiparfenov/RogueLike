using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class WarriorBlow : MonoBehaviour
{
    // Start is called before the first frame update
    public float blowDuration;
    public float damage = 3;
    void Start()
    {
        Invoke("DestroySelf", blowDuration);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Enemy"))
        {
            return;
        }

        var damageable = col.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(damage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
