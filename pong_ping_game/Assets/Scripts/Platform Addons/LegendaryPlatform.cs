using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PongPing
{

    public class LegendaryPlatform : PlatformAddon
    {
        public override void Activate(GameObject objectInScene)
        {
            objectInScene.transform.GetChild(0).gameObject.GetComponent<Light>().intensity = Mathf.Sin(Time.time * 15f) * 52;
        }
        public override void Disable(GameObject objectInScene)
        {
            objectInScene.transform.GetChild(0).gameObject.GetComponent<Light>().intensity = 42f;
        }
    }
}