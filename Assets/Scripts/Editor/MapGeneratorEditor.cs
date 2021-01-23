using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();
        
        MapGenerator mapGen = (MapGenerator)target;

        if(GUILayout.Button("Generate Map")) {
            mapGen.GenerateAndDisplayMap();
        }
    }
}
