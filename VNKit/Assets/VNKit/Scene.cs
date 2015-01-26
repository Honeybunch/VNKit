using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VNKit
{
    public class Scene : MonoBehaviour
    {

        public string Title;
        public List<Background> Backgrounds = new List<Background>();

        public bool StartScene = false;

        public GameObject PrevSceneObject = null;
        public GameObject NextSceneObject = null;

        [HideInInspector]
        public float overviewX;
        [HideInInspector]
        public float overviewY;

        private Action[] actions;
        private int actionCounter = -1;

        // Use this for initialization
        void Start()
        {
            //Get the unordered scene actions and order them
            Action[] unorderedActions = GetComponents<Action>();

            actions = new Action[unorderedActions.Length];

            foreach (Action ua in unorderedActions)
                actions[ua.ActionIndex] = ua;
        }

        public void TriggerAction(int actionIndex) 
        {
            if (actionIndex >= 0 && actionIndex < actions.Length)
            {
                actionCounter = actionIndex;
                actions[actionIndex].Trigger();
            }
            else
            {
                Debug.LogError("Cannot execute action - " + actionIndex + ". It doesn't exist in this scene!");
            }
        }

        public Action GetPrevAction()
        {
            if (actionCounter > 1)
                return actions[actionCounter - 1];
            else
                return null;
        }

        public Action GetNextAction()
        {
            if (actionCounter < actions.Length - 1)
                return actions[actionCounter + 1];
            
            else
                return null;
        }
        
    }
}
