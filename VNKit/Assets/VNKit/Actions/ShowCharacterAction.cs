using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VNKit
{
    public class ShowCharacterAction : Action
    {
        //The character performing the actions
        public Character Character;
        public Vector2 Position;

        public override void Trigger()
        {
            Character.gameObject.transform.position = new Vector3(Position.x, Position.y, 0);
        }

    }
}
