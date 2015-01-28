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

		private static bool showBackgroundFoldout = false;

        /// <summary>
        /// Called when the Scene is selected; used to make sure that variables are up to date
        /// </summary>
        public void OnEnable()
        {
            scene = (Scene)target;

            backgrounds = scene.Backgrounds;

            UpdateBackgrounds();
        }

		/// <summary>
		/// Draws the scene inspector
		/// Have a static function for this so we can draw it anywhere we want
		/// </summary>
		/// <param name="scene">The scene that we want to draw the inspector of</param>
		public static void DrawSceneInspector(Scene scene)
		{
			List<Background> backgrounds = scene.Backgrounds;
			Action[] actions = scene.GetComponents<Action>();

			GUILayout.BeginHorizontal();
			GUILayout.Label("Title");
			GUILayout.FlexibleSpace();
			scene.Title = GUILayout.TextField(scene.Title, GUILayout.Width(150));
			scene.name = scene.Title;
			GUILayout.EndHorizontal();
			
			GUILayout.Space(6);

			showBackgroundFoldout = EditorGUILayout.Foldout(showBackgroundFoldout, "Backgrounds");
			if(showBackgroundFoldout)
			{
				for(int i = 0; i < backgrounds.Count; i++)
				{
					GUILayout.BeginHorizontal();

					GUILayout.Label("    Element " + i);

					GUILayout.FlexibleSpace();

					if(backgrounds[i] != null)
						backgrounds[i] = EditorGUILayout.ObjectField(backgrounds[i], backgrounds[i].GetType(), true) as Background;

					GUILayout.EndHorizontal();
				}

				//Always draw one last object field for the user to add their own existing background from the Unity Scene
				GUILayout.BeginHorizontal();
				
				GUILayout.Label("    Element " + backgrounds.Count);
				
				GUILayout.FlexibleSpace();

				Background newBackground = EditorGUILayout.ObjectField(null, typeof(Background), true) as Background;

				if(newBackground != null)
					backgrounds.Add(newBackground);
				
				GUILayout.EndHorizontal();

			}
			//Set the backgrounds that were modified back to the Scene object
			scene.Backgrounds = backgrounds;

			if (GUILayout.Button("Add New Background"))
				NewBackgroundWizard.CreateWizard(scene.gameObject);
		}

        public override void OnInspectorGUI()
        {
			DrawSceneInspector(scene);
        }

        /// <summary>
        /// Update the backgrounds in the scene to reflect the changes made in the inspector
        /// </summary>
        private void UpdateBackgrounds()
        {
            List<Background> newBackgrounds = scene.gameObject.transform.GetComponentsInChildren<Background>().ToList<Background>();

			//If any backgrounds are set to null, remove them
			foreach(Background b in newBackgrounds)
			{
				if(b == null)
					newBackgrounds.Remove(b);
			}

            scene.Backgrounds = newBackgrounds;
        }
	
    }
}
