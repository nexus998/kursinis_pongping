using UnityEngine;
namespace PongPing
{
    public class PlatformController
    {
        //can toggle movement. if false, then cant move platform.
        bool canMove = true;
        public void ToggleMovement(bool enable) => canMove = enable;

        public void MovePlatform(bool up, float speed, GameObject translationObject)
        {
            //get screen pos from world pos
            Vector3 screenPos = Camera.main.WorldToScreenPoint(translationObject.transform.position);
            if (canMove)
            {
                if (up)
                {
                    //make sure you cant go out of bounds. (up)
                    if (screenPos.y < Screen.height - 100)
                        translationObject.transform.position = new Vector3(translationObject.transform.position.x, translationObject.transform.position.y + speed * Time.deltaTime, translationObject.transform.position.z);
                }
                else
                {
                    //down
                    if (screenPos.y > 160)
                        translationObject.transform.position = new Vector3(translationObject.transform.position.x, translationObject.transform.position.y - speed * Time.deltaTime, translationObject.transform.position.z);
                }
            }
        }
    }
}