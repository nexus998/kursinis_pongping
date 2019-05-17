using UnityEngine;
namespace PongPing
{
    public class PlatformController
    {
        public void MovePlatform(bool up, float speed, GameObject translationObject)
        {
            if(up) translationObject.transform.position = new Vector3(translationObject.transform.position.x, translationObject.transform.position.y + speed * Time.deltaTime, translationObject.transform.position.z);
            else translationObject.transform.position = new Vector3(translationObject.transform.position.x, translationObject.transform.position.y - speed * Time.deltaTime, translationObject.transform.position.z);
        }
    }
}