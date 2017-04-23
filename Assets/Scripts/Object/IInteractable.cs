using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IObject
{
    public interface IInteractable
    {
        void SetupButtons(GameObject prefab, Transform parent);
    }
}