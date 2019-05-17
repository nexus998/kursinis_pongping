using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PongPing
{
    public class Player : MonoBehaviour
    {
        public static int movementSpeed = 10;
        private int score;
        public int GetScore() => score;
        public void UpScore() { score++; }
        public KeyCode upKey = KeyCode.W, downKey = KeyCode.S;
        public GameObject objectInScene;

        public enum playerTypes { player, pc };
        public playerTypes playerType;

        [Header("AI Settings")]
        public float detectDistance = 5.5f;

        private void Awake()
        {
            if (!objectInScene) objectInScene = gameObject;
            controller = new PlatformController();
        }
        PlatformController controller;
        private void PlayerMovement()
        {
            //controller = new PlatformController();
            if (Input.GetKey(upKey))
            {
                controller.MovePlatform(up: true, speed: movementSpeed, translationObject: objectInScene);
            }
            if (Input.GetKey(downKey))
            {
                controller.MovePlatform(up: false, speed: movementSpeed, translationObject: objectInScene);
            }
        }
        private void AIMovement()
        {
            float distance = objectInScene.transform.position.x - GameObject.Find("Ball").transform.position.x;
            Debug.Log("Distance: " + distance);
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

        private void FixedUpdate()
        {
            if (playerType == playerTypes.player)
            {
                PlayerMovement();
            }
            else
            {
                AIMovement();
            }
        }
    }
}