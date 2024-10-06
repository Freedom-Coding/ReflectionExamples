using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReflectionExample
{
    public class TypeSelector : MonoBehaviour
    {
        public Type selectedType;
        public Type filterType = typeof(StatePattern.IEnemyState);

        private void Update()
        {
            if (selectedType != null)
            {
                Debug.Log(selectedType.ToString());
            }
        }
    }
}