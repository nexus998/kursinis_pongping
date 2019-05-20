using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PongPing
{
    public class EpicPlatform : PlatformAddon
    {
        public override void Activate(GameObject objectInScene)
        {
            objectInScene.transform.GetChild(0).gameObject.GetComponent<Light>().intensity = Mathf.Sin(Time.time * 3f) * 42;
        }
        public override void Disable(GameObject objectInScene)
        {
            objectInScene.transform.GetChild(0).gameObject.GetComponent<Light>().intensity = 42f;
        }
    }
}