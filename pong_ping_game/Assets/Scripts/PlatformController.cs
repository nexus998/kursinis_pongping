using UnityEngine;
namespace PongPing
{
    public class PlatformController
    {
        bool canMove = true;
        public void ToggleMovement(bool enable) => canMove = enable;

        public void MovePlatform(bool up, float speed, GameObject translationObject)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(translationObject.transform.position);
            if (canMove)
            {
                if (up)
                {
                    if (screenPos.y < Screen.height - 50)
                        translationObject.transform.position = new Vector3(translationObject.transform.position.x, translationObject.transform.position.y + speed * Time.deltaTime, translationObject.transform.position.z);
                }
                else
                {
                    if (screenPos.y > 50)
                        translationObject.transform.position = new Vector3(translationObject.transform.position.x, translationObject.transform.position.y - speed * Time.deltaTime, translationObject.transform.position.z);
                }
            }
        }
    }
}