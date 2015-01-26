using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VNKit
{
    class NewSceneWizard : ScriptableWizard
    {
        public string Title;
        public bool AutoSizeBackgrounds = true;
        public List<Sprite> Backgrounds = new List<Sprite>(1);

        public delegate void CreateSceneDelegate(GameObject sceneObject);

        CreateSceneDelegate createSceneDelegate;

        /// <summary>
        /// Create a wizard!
        /// </summary>
        public static void CreateWizard(CreateSceneDelegate delegateMethod)
        {
            NewSceneWizard wizard = ScriptableWizard.DisplayWizard<NewSceneWizard>("Create New Scene", "Create", "Cancel");
            wizard.createSceneDelegate = delegateMethod;
        }

        /// <summary>
        /// Create the new scene object
        /// </summary>
        void OnWizardCreate()
        {
            if (string.IsNullOrEmpty(Title))
                Title = "New Scene";

            GameObject sceneObject = new GameObject(Title);
            sceneObject.AddComponent<Scene>();

            Scene scene = sceneObject.GetComponent<Scene>();

            scene.Title = Title;

            //Create sprites for every background and make them children of the scene object
            foreach (Sprite s in Backgrounds)
            {
                GameObject backgroundObject = new GameObject();
                backgroundObject.name = s.name;
                
                SpriteRenderer spriteRenderer = backgroundObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = s;

                Background background = backgroundObject.AddComponent<Background>();
                
                if(AutoSizeBackgrounds)
                    background.SizeToFitScreen();

                //Keep the backgrounds on their scene parent
                backgroundObject.transform.parent = scene.transform;

                scene.Backgrounds.Add(background);
            }

            createSceneDelegate(sceneObject);
        }

        /// <summary>
        /// Cancel and do nothing
        /// </summary>
        void OnWizardOtherButton()
        {
            this.Close();
        }
    }

}