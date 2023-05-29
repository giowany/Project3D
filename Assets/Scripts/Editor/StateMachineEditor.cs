using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;

[CustomEditor(typeof(FSMExemple))]
public class StateMachineEditor : Editor
{
    public bool showFoldout;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FSMExemple fsm = (FSMExemple)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if (fsm.machine == null) return;

        if(fsm.machine.CurrentState != null)
            EditorGUILayout.LabelField("Current State", fsm.machine.CurrentState.ToString());

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Avaiable States");

        if (showFoldout)
        {
            if(fsm.machine.ditionaryStates != null)
            {
                var keys = fsm.machine.ditionaryStates.Keys.ToArray();
                var values = fsm.machine.ditionaryStates.Values.ToArray();

                for(int i = 0; i< keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], values[i]));
                }

            }
        }

    }
}
