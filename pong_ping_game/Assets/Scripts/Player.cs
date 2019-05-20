using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PongPing
{
    public class Player
    {
        private const int movementSpeed = 10;
        private int score;
        public enum playerTypes { player, pc };
        private playerTypes playerType;
        private float detectDistance = 5.5f;
        private PlatformController controller = new PlatformController();

        public string GetControlMode() => playerType.ToString();
        public int GetScore() => score;
        public void UpScore() { score++; }
        public GameObject objectInScene;
        private Controls controls;
     
        public void PlayerMovement()
        {
            if (controls.IfKeyHeld(controls.GetUpKey()))
            {
                controller.MovePlatform(up: true, speed: movementSpeed, translationObject: objectInScene);
            }
            if (controls.IfKeyHeld(controls.GetDownKey()))
            {
                controller.MovePlatform(up: false, speed: movementSpeed, translationObject: objectInScene);
            }
        }
        public void AIMovement()
        {
            float distance = objectInScene.transform.position.x - GameObject.Find("Ball").transform.position.x;
            if (distance <= detectDistance)
            {
                if (objectInScene.transform.position.y > GameObject.Find("Ball").transform.position.y)
                {
                    controller.MovePlatform(up: false, movementSpeed, objectInScene);
                }
                if (objectInScene.transform.position.y < GameObject.Find("Ball").transform.position.y)
                {
                    controller.MovePlatform(up: true, movementSpeed, objectInScene);
                }
            }
        }
        public void SwitchControlMode()
        {
            if (playerType == playerTypes.pc) playerType = playerTypes.player;
            else playerType = playerTypes.pc;
        }

        public Player(GameObject objectInScene, playerTypes controlMode, Controls controlSetup)
        {
            this.objectInScene = objectInScene;
            playerType = controlMode;
            controls = controlSetup;
        }
    }
}