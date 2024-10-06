using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ReflectionExample
{
    public class StateFactory
    {
        private Dictionary<Type, IEnemyState> _states = new Dictionary<Type, IEnemyState>();

        public object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public IEnemyState GetState<T>() where T : IEnemyState, new()
        {
            Type stateType = typeof(T);

            if (_states.ContainsKey(stateType))
            {
                return _states[stateType];
            }

            object stateObject = Activator.CreateInstance(stateType);
            T stateInstance = (T)stateObject;

            _states.Add(stateType, stateInstance);
            return stateInstance;
        }
    }
}