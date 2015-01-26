using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace VNKit
{
	
	[CustomEditor(typeof(Action), true)]
	public class ActionEditor : Editor
	{
		private Action action;

		private int previousActionIndex;
		private int editingActionIndex;
		private bool editing = false;

		/// <summary>
		/// Called when the scene is selected; used to save a copy of the action for later manipulation
		/// </summary>
		public void OnEnable()
		{
			action = (Action)target;
		}

		/// <summary>
		/// Custom inspector to allow every action to easily be moved up and down in the heirarchy
		/// </summary>
		public override void OnInspectorGUI()
		{
			//Draw the Action Index label and modification buttons
			GUILayout.BeginHorizontal ();
			{
				GUILayout.Label ("Action");

				EditorGUILayout.Space();
				EditorGUILayout.Space();

				if(GUILayout.Button("Move Up"))
					MoveUp();

				if(!editing)
				{
					editingActionIndex = EditorGUILayout.IntField(action.ActionIndex);
					if(editingActionIndex != action.ActionIndex)
					{
						previousActionIndex = action.ActionIndex;
						editing = true;
					}
				}
				else
				{
					editingActionIndex = EditorGUILayout.IntField(editingActionIndex);
					if(Event.current.isKey && Event.current.keyCode == KeyCode.Return)
					{
						MoveAction(editingActionIndex);
						editing = false;
					}
					else if(Event.current.isKey && Event.current.keyCode == KeyCode.Escape || Event.current.isMouse)
					{
						editing = false;
					}
				}


				if(GUILayout.Button("Move Down"))
					MoveDown();
			}
			GUILayout.EndHorizontal ();

			EditorGUILayout.Space ();
			EditorGUILayout.Space ();

			base.OnInspectorGUI ();
		}

		/// <summary>
		/// Moves the action in the heirarchy by calling MoveUp or MoveDown enough times
		/// </summary>
		/// <param name="targetIndex">Target index.</param>
		public void MoveAction(int targetIndex)
		{
			//Don't do anything if nothing changed
			if (targetIndex == action.ActionIndex)
				return;

			Action[] actions = action.GetComponents<Action> ();
			
			//If the target index is too high or too low, lets adjust the target
			if (targetIndex <= 0)
				targetIndex = 0;
			else if (targetIndex >= (actions.Length - 1))
				targetIndex = (actions.Length - 1);
			
			//Determine if we're going up or down; true is up, down is false
			int difference = (targetIndex - action.ActionIndex);
			bool upOrDown = difference < 0;
			 
			for(int i =0; i < Mathf.Abs(difference); i++)
			{
				if(upOrDown)
					MoveUp();
				else
					MoveDown();
			}
		}

		/// <summary>
		/// Moves this action up the heirarchy (executes earlier)
		/// </summary>
		public void MoveUp()
		{
			//If this action is already at index 0, we can't go any further up
			if (action.ActionIndex == 0)
				return;

			Action[] actions = action.GetComponents<Action> ();

			//Get the action previous to this one because we'll be swapping with it
			Action prevAction = actions [action.ActionIndex - 1];

			action.ActionIndex--;
			prevAction.ActionIndex++;

			//Move the action in the inspector
			UnityEditorInternal.ComponentUtility.MoveComponentUp(action);
		}

		/// <summary>
		/// Moves this action down the heirarchy (executes later)
		/// </summary>
		public void MoveDown()
		{
			Action[] actions = action.GetComponents<Action> ();

			//If this action is already the last one, we can't move it down any further
			if (action.ActionIndex + 1 == actions.Length)
				return;

			//Get the action previous to this one because we'll be swapping with it
			Action nextAction = actions [action.ActionIndex + 1];
			
			action.ActionIndex++;
			nextAction.ActionIndex--;
			
			//Move the action in the inspector
			UnityEditorInternal.ComponentUtility.MoveComponentDown(action);
		}
	}
}
