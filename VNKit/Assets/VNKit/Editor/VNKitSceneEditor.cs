using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace VNKit
{

    [CustomEditor(typeof(Scene))]
    public class SceneEditor : Editor
    {

        private List<Background> backgrounds = new List<Background>(1);

        private Scene scene;

        /// <summary>
        /// Called when the Scene is selected; used to make sure that variables are up to date
        /// </summary>
        public void OnEnable()
        {
            scene = (Scene)target;

            backgrounds = scene.Backgrounds;

            UpdateBackgrounds();
        }


        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUILayout.Space();

            if (GUILayout.Button("Add Background"))
                NewBackgroundWizard.CreateWizard(scene.gameObject);

        }

        /// <summary>
        /// Update the backgrounds in the scene to reflect the changes made in the inspector
        /// </summary>
        private void UpdateBackgrounds()
        {
            List<Background> newBackgrounds = scene.gameObject.transform.GetComponentsInChildren<Background>().ToList<Background>();

            scene.Backgrounds = newBackgrounds;
        }
	
    }
}
