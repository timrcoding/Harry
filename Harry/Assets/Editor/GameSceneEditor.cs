using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]

public class GameSceneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager gameManager = (GameManager)target;

        if (GUILayout.Button("SelectDialogue"))
        {
            GameManager.instance.StartDialogue(GameManager.instance.m_OverrideSubject);
        }
    }
}
