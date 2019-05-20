using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PongPing
{
    //used to make addons for skins. they must inherit from this and override functions.
    public abstract class PlatformAddon
    {
        public abstract void Activate(GameObject objectInScene);
        public virtual void Disable(GameObject objectInScene)
        {
            objectInScene.transform.GetChild(0).gameObject.GetComponent<Light>().intensity = 42f;
        }
    }
}