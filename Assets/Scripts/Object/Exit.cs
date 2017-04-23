using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IObject
{
    public class Exit : InteractableObject
    {

        public override void SetupButtons(GameObject prefab, Transform parent)
        {
            GenerateButton(prefab, parent, "Leave", Leave);
        }

        void Leave()
        {
            mouseController.ManualUnselect();
            if (actionCenter.heroFriend.isOutside)
            {
                generator.NewMessage(actionCenter.heroFriend.name + " is still outside. Wait for her to exit.");
            }
            ss.GameEnd();
        }

        public StartScript ss;

    }
}