using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VNKit
{
    class NewBackgroundWizard : ScriptableWizard
    {
        public Sprite Sprite = null;
        public bool AutoSize = true;
        public Vector3 Position = Vector3.zero;


        private static GameObject Parent;

        /// <summary>
        /// Create a wizard!
        /// </summary>
        public static void CreateWizard(GameObject parent)
        {
            Parent = parent;
            ScriptableWizard.DisplayWizard<NewBackgroundWizard>("Create New Background", "Create", "Cancel");
        }

         /// <summary>
        /// Create the new background object
        /// </summary>
        void OnWizardCreate()
        {
            GameObject newBackground = new GameObject();
            Background background = newBackground.AddComponent<Background>();
            SpriteRenderer spriteRenderer = newBackground.AddComponent<SpriteRenderer>();

            if(Sprite)
                newBackground.name = Sprite.name;

            newBackground.transform.position = Position;
            background.AutoSize = AutoSize;
            spriteRenderer.sprite = Sprite;

            newBackground.transform.parent = Parent.transform;

            if(AutoSize)
                background.SizeToFitScreen();
        }
    }

}