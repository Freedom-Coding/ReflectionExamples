using UnityEngine;
using System;
using System.Reflection;

namespace ReflectionExample
{
    public class PluginLoader : MonoBehaviour
    {
        public void LoadPlugin(string pathToDll)
        {
            Assembly assembly = Assembly.LoadFile(pathToDll);
            Type pluginType = assembly.GetType("PluginNamespace.PluginClass");
            MethodInfo startMethod = pluginType.GetMethod("Start");

            object pluginInstance = Activator.CreateInstance(pluginType);
            startMethod.Invoke(pluginInstance, null);
        }
    }
}