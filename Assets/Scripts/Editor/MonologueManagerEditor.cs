using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonologueManager))]
public class MonologueManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MonologueManager mm = (MonologueManager)target;

        if(GUILayout.Button("Perform Debug"))
        {
            mm.DebugFunction();
        }
    }
}
