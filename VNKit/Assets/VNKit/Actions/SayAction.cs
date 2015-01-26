using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VNKit
{
    public class SayAction : Action
    {
        //The character performing the actions
        public Character character;
        public string Text;

        public override void Trigger()
        {
            if (Master.LastCharacterToSpeak == character)
                Master.DisplayText(" " + Text);
            else if (Master.LastCharacterToSpeak != character)
                Master.DisplayText("\n" + character.name + " : " + Text);

            Master.LastCharacterToSpeak = character;
        }

    }
}
