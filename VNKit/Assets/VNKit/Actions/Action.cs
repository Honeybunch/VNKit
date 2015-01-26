using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace VNKit
{
    public abstract class Action : MonoBehaviour
    {
        [HideInInspector]
        public VNKitMaster Master;

		[HideInInspector]
        public int ActionIndex = 0;

        public bool AutoTrigger = false;

        //Time to spend waiting before automatically triggering
        public float AutoTriggerWaitTime = 0;

        public void Start()
        {
            Master = GameObject.FindObjectOfType<VNKitMaster>();
        }

        public abstract void Trigger();

    }
}
