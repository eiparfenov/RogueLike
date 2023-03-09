using System;
using System.Globalization;
using UnityEngine;

namespace Example.Signals
{
    [Serializable]
    public class ExampleSignal
    {
        [field: SerializeField] public float SomeNumber { get; private set; }
        public ExampleSignal(float someNumber)
        {
            SomeNumber = someNumber;
        }
        public override string ToString()
        {
            return SomeNumber.ToString(CultureInfo.InvariantCulture);
        }
    }
}