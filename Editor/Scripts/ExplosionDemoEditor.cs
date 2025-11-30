// -----------------------------------------------------------------------
//  <copyright file="ExplosionDemoEditor.cs">
//  Copyright (c) 2025 AkagawaTsurunaki. All rights reserved.
//  Licensed under the MIT License.
//  </copyright>
//  <author>AkagawaTsurunaki (AkagawaTsurunaki@outlook.com)</author>
// -----------------------------------------------------------------------

using KoumakanModelExploder.Scripts.ModelExploder;
using UnityEditor;
using UnityEngine;

namespace KoumakanModelExploder.Editor.Scripts
{
    [CustomEditor(typeof(Exploder))]
    public class ExplosionDemoEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            EditorGUILayout.Space();
            var demo = (Exploder)target;

            GUILayout.BeginVertical();
            if (GUILayout.Button("Print Model Hiarachy")) demo.PrintTree();
            if (GUILayout.Button("Calculate Target Layout")) demo.CalculateLayout();
            if (GUILayout.Button("Explode!")) demo.ApplyAnimation();
            if (GUILayout.Button("Undo")) demo.RevertAnimation();
            GUILayout.EndVertical();
        }
    }
}