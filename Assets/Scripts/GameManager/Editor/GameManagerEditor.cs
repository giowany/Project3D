using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public bool showFoldout;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager gm = (GameManager)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if (gm.stateMachine == null) return;

        if (gm.stateMachine.CurrentState != null)
            EditorGUILayout.LabelField("Current State", gm.stateMachine.CurrentState.ToString());

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Avaiable States");

        if (showFoldout)
        {
            if (gm.stateMachine.ditionaryStates != null)
            {
                var keys = gm.stateMachine.ditionaryStates.Keys.ToArray();
                var values = gm.stateMachine.ditionaryStates.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], values[i]));
                }

            }
        }

    }
}
