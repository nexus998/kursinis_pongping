using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PongPing
{
    public abstract class PlatformAddon
    {
        public abstract void Activate(GameObject objectInScene);
        public abstract void Disable(GameObject objectInScene);
    }
}