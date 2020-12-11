/*
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SoundPoint))]
public class SoundPointEditor : Editor
{
    public SoundPoint selected;

    private void OnEnable()
    {
        selected = (SoundPoint)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Create New Sound Point"))
        {
            selected.AddNewPoint();
        }
    }
}
*/
