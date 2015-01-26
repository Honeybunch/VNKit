using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace VNKit
{

    [CustomEditor(typeof(Background))]
    public class BackgroundEditor : Editor
    {
        private Background background;

        /// <summary>
        /// Called when background is selected; used to update variables
        /// </summary>
        public void OnEnable()
        {
            background = (Background)target;

            EditorApplication.update += Update;
        }

        /// <summary>
        /// Called when the object is no longer in focus; used detach the update event
        /// </summary>
        public void OnDisable()
        {
            EditorApplication.update -= Update;
        }

        /// <summary>
        /// Called many times per second; used to make sure that if auto-size is enabled that the background is resized
        /// </summary>
        public void Update() 
        {
            //It's possible that if you delete the background that this function could stil get called; this is to make sure no unnecessary errors are thrown
            if (!background)
                return;

            background.Update();
        }

        /// <summary>
        /// Used to draw custom GUI elements
        /// </summary>
        public override void OnInspectorGUI() 
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

            if (GUILayout.Button("Size To Camera"))
                background.SizeToFitScreen();
        }


    }
}
