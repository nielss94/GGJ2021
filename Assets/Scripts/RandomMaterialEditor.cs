using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RandomMaterialEditor : EditorWindow
{
    
    [MenuItem("Piel Entertainment/Random Material")]
    private static void ShowWindow() {
        GetWindow<RandomMaterialEditor>();
    }

    public List<Material> materials = new List<Material>();

    void OnGUI()
    {
        // "target" can be any class derrived from ScriptableObject 
        // (could be EditorWindow, MonoBehaviour, etc)
        ScriptableObject target = this;
        SerializedObject so = new SerializedObject(target);
        SerializedProperty materialsProperty = so.FindProperty("materials");
        
        EditorGUILayout.BeginVertical();
        EditorGUILayout.PropertyField(materialsProperty, true); // True means show children

        if (GUILayout.Button("Randomize Materials"))
        {
            RandomMaterial[] allObjectWithRandomMaterial = FindObjectsOfType<RandomMaterial>();

            foreach (var objectWithRandomMaterial in allObjectWithRandomMaterial)
            {
                objectWithRandomMaterial.Apply(materials[Random.Range(0, materials.Count)]);
            }
        }
        EditorGUILayout.EndVertical();
        
        so.ApplyModifiedProperties(); // Remember to apply modified properties
    }
}
