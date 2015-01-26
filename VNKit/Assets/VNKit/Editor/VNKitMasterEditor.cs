using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VNKit
{
    [CustomEditor(typeof(VNKitMaster))]
    public class VNKitMasterEditor : Editor
    {
        //The master we're attached to
        VNKitMaster master;

        /// <summary>
        /// Handles the drawing and logic for the custom inspector
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Show Scene Overview"))
            {
                VNKitOverviewWindow.ShowWindow();
            }
        }
    }
}
