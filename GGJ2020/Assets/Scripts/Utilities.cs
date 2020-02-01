using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This Class Contains read made methods for use anytime list of Methods::
///  - SetLayerRecursively: to set the layers of all gameobjects children under a parent gameObject 
///  - ...
/// </summary>
public static class Utilities  {

    public static void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

}
