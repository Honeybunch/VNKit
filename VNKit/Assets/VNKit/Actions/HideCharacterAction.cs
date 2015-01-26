using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VNKit
{
    public class HideCharacterOption : Action
    {
        public Character Character;
        public Vector2 Position;

        public override void Trigger()
        {
            Character.gameObject.transform.position = new Vector3(0, 0, -1);
        }

    }
}
