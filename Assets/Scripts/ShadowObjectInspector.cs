using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShadowObject))]
public class ShadowObjectInspector : Editor {
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        if (GUILayout.Button("Set solve rotation from object")) {
            var shadowObject = target as ShadowObject;
            shadowObject.solveRotation = shadowObject.transform.rotation;
        }
    }
}