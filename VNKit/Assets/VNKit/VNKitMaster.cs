using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace VNKit
{
    public class VNKitMaster : MonoBehaviour
    {
        public GUISkin CurrentSkin;

        [HideInInspector]
        public Character LastCharacterToSpeak;

        private Rect textArea;

        private Scene[] scenes;

        private Scene currentScene;
        private string currentText;

        private bool waiting = false;

        /// <summary>
        /// Finds all the scenes and sets up the flow of the game
        /// </summary>
        void Start()
        {
            scenes = GameObject.FindObjectsOfType<Scene>();

            //Find the start scene
            foreach (Scene s in scenes) 
            {
                if (s.StartScene)
                {
                    currentScene = s;
                    break;
                }
            }
        }

        /// <summary>
        /// Check for input and advance the scene accordingly
        /// </summary>
        void Update() 
        {
            if (waiting)
                return;

            //Get the next action
            Action action = currentScene.GetNextAction();

            if (action != null)
            {

                if (action.AutoTrigger)
                {
                    if (action.AutoTriggerWaitTime > 0)
                        StartCoroutine(DelayedAction(action.AutoTriggerWaitTime, action.ActionIndex));   
                    
                    else
                        currentScene.TriggerAction(action.ActionIndex);
                    
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                        currentScene.TriggerAction(action.ActionIndex);
                }
            }
            else 
            {
                
            }
        }

        /// <summary>
        /// Used to show text
        /// </summary>
        public void OnGUI() 
        {
            if (CurrentSkin)
                GUI.skin = CurrentSkin;

            GUIStyle style = CurrentSkin.GetStyle("label");

            GUIStyle backupStyle = style;

            style.normal.textColor = Color.black;

            Rect position = new Rect(6, Screen.height - 200, Screen.width - 12, 194);

            int border = 1;

            position.x -= border;
            position.y += border;
            GUI.Label(position, currentText, style);
            position.x += border * 2;
            GUI.Label(position, currentText, style);
            position.y -= border;
            GUI.Label(position, currentText, style);
            position.x -= border * 2;
            GUI.Label(position, currentText, style);
            position.x += border;

            style.normal.textColor = Color.white;
            GUI.Label(position, currentText, style);

            style = backupStyle;
            
        }

        /// <summary>
        /// Used for triggers to display text uniformly
        /// </summary>
        /// <param name="text">Text to display</param>
        public void DisplayText(string text) 
        {
            currentText += text;
        }

        /// <summary>
        /// Run a coroutine for a given amount of time that will block any other updates or scene changes before firing a specified action
        /// </summary>
        /// <param name="seconds">seconds to wait until action is executed</param>
        /// <param name="actionIndex">action index to execute in the scene</param>
        private IEnumerator DelayedAction(float seconds, int actionIndex)
        {
            waiting = true;
            yield return new WaitForSeconds(seconds);
            waiting = false;

            currentScene.TriggerAction(actionIndex);
        }

    }
}
