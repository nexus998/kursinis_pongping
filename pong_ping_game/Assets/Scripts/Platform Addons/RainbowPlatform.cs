using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PongPing
{
    public class RainbowPlatform : PlatformAddon
    {
        float timer = 0;
        Color newColor = Color.red;
        public override void Activate(GameObject objectInScene)
        {
            timer += Time.deltaTime;
            if(timer > 0.3f)
            {
                newColor = Random.ColorHSV();
                timer = 0;
            }
            objectInScene.transform.GetChild(0).gameObject.GetComponent<Light>().color = Color.Lerp(objectInScene.transform.GetChild(0).gameObject.GetComponent<Light>().color,
                newColor, Time.deltaTime * 3f);
        }
        public override void Disable(GameObject objectInScene)
        {
            objectInScene.transform.GetChild(0).gameObject.GetComponent<Light>().intensity = 42f;
        }
    }
}
