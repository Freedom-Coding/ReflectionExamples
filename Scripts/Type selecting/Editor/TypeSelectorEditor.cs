using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Reflection;

namespace ReflectionExample
{
    [CustomEditor(typeof(TypeSelector))]
    public class TypeSelectorEditor : Editor
    {
        private Type[] assemblyTypes;
        private string[] typeNames;
        private int selectedIndex = 0;

        private void OnEnable()
        {
            // Get all types in the main game assembly (Assembly-CSharp)
            Assembly gameAssembly = Assembly.Load("Assembly-CSharp");
            
            // Get all types from the game assembly
            assemblyTypes = gameAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract) // Filter for non-abstract classes
                .ToArray();

            TypeSelector selector = (TypeSelector)target;

            // Filter the types based on the filterType
            if (selector.filterType != null)
            {
                assemblyTypes = assemblyTypes.Where(t =>
                    selector.filterType.IsAssignableFrom(t)).ToArray();
                typeNames = assemblyTypes.Select(t => t.Name).ToArray();
            }
            else
            {
                typeNames = assemblyTypes.Select(t => t.FullName).ToArray();
            }

            // Find the currently selected type in the dropdown
            selectedIndex = Array.FindIndex(assemblyTypes, t => t == selector.selectedType);
        }

        public override void OnInspectorGUI()
        {
            // Draw the dropdown to select a type
            EditorGUILayout.LabelField("Select a Type:");
            selectedIndex = EditorGUILayout.Popup(selectedIndex, typeNames);

            if (selectedIndex >= 0 && selectedIndex < typeNames.Length)
            {
                TypeSelector selector = (TypeSelector)target;
                selector.selectedType = assemblyTypes[selectedIndex];
                EditorUtility.SetDirty(selector); // Mark the object as dirty so changes are saved
            }

            // Draw the default inspector elements (optional)
            DrawDefaultInspector();
        }
    }
}