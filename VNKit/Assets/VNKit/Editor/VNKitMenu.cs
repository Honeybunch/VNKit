using UnityEditor;
using UnityEngine;
using System.Collections;

namespace VNKit
{
    public class VNKitMenu : Editor
    {

        //Some parameters dealing with scene setup
        public static string MainGameObjectName = "VNKitMaster";

        [MenuItem("VNKit/New Game")]

        //Creates a new "Game" inside the Unity Scene; 
        //The subsystems are controlled by a script on a main game object 
        //This menu item will set the scene up with the right scripts to get started
        public static void NewGame()
        {
            //Try to find an existing game master and if it exists check if the user wants to overwrite it
            GameObject existingMaster = GameObject.Find(MainGameObjectName);

            if (existingMaster != null)
            {
                if (EditorUtility.DisplayDialog("Existing VNKit Game!", "There is already a " + MainGameObjectName + " in the scene. Do you want to overwrite it?", "Yes", "No"))
                {
                    if (EditorUtility.DisplayDialog("Existing VNKit Game!", "This will remove everything in the scene, deleting all your work. Are you sure you want to reset it?", "Yes", "No"))
                    {
                        ResetScene();
                        CreateNewGame();
                    }
                }
            }
            else
            {
                CreateNewGame();
            }

        }

        [MenuItem("VNKit/Show Scene Overview")]
        public static void ShowSceneOverview()
        {
            GameObject existingMaster = GameObject.Find(MainGameObjectName);

            if (existingMaster == null)
                EditorUtility.DisplayDialog("No VNKit Game Found", "There was no existing VNKit Game found in the Unity scene so we cannot load the scene overview.", "Okay");
            else
                VNKitOverviewWindow.ShowWindow();
        }

        /// <summary>
        /// Clears the scene of all objects and then adds default objects to it
        /// </summary>
        private static void ResetScene()
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

            for (int i = 0; i < allObjects.Length; i++)
                DestroyImmediate(allObjects[i]);

            GameObject camera;
            camera = new GameObject("MainCamera");
            camera.AddComponent("Camera");
            camera.tag = "MainCamera";
            camera.transform.position = new Vector3(0, 0, -10);

            GameObject light;
            light = new GameObject("Point light");
            light.AddComponent<Light>();
        }

        /// <summary>
        /// Creates the master game object for VNKit and applies all necessary components to it
        /// </summary>
        private static void CreateNewGame()
        {
            GameObject newMasterObject = new GameObject(MainGameObjectName);
            newMasterObject.AddComponent<VNKitMaster>();
        }

    }

}