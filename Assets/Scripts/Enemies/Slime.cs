using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Enemies
{
    public class Slime : StrangeStalker
    {
        protected override void Death()
        {
            base.Death();
            Destroy(audio);
        }
    }
}
