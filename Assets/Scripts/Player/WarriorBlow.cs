using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorBlow : MonoBehaviour
{
    // Start is called before the first frame update
    public float blowDuration = 0.2f;
    void Start()
    {
        Invoke("DestroySelf", blowDuration);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
