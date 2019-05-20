using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PongPing
{
    public abstract class PlatformAddon
    {
        public virtual void Activate(GameObject objectInScene)
        {
            objectInScene.transform.GetChild(0).gameObject.GetComponent<Light>().intensity = 94f;
        }
        public virtual void Disable(GameObject objectInScene)
        {
            objectInScene.transform.GetChild(0).gameObject.GetComponent<Light>().intensity = 42f;
        }
    }
}