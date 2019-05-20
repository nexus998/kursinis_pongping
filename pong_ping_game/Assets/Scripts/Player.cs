using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PongPing
{
    public class Player
    {
        private const int movementSpeed = 10;
        private int score;
        public enum PlayerTypes { player, pc };
        private PlayerTypes playerType;
        private readonly float detectDistance = 6.5f;
        private PlatformController controller = new PlatformController();

        public string GetControlMode() => playerType.ToString();
        public int GetScore() => score;
        public void UpScore() { score++; }
        public GameObject objectInScene;
        private Controls controls;
        private Skin currentSkin = null;
        public Skin GetCurrentSkin() => currentSkin;

        public void ChangeSkin(int skinID)
        {
            if (currentSkin != null)
            {
                if(currentSkin.GetAddonModule() != null)
                    currentSkin.GetAddonModule().Disable(objectInScene);
            }
            SkinSelector selector = new SkinSelector();
            if(SkinSelector.skins.Count >= skinID-1)
            {
                currentSkin = SkinSelector.skins[skinID];
                objectInScene.transform.GetChild(0).gameObject.GetComponent<Light>().color = SkinSelector.skins[skinID].GetColor();
                objectInScene.GetComponent<SpriteRenderer>().color = SkinSelector.skins[skinID].GetColor();
            }
            else
            {
                Debug.LogError("Skin with ID of " + skinID + " does not exist!");
            }
        }
        public void ChangeSkin(Skin skin)
        {
            if (currentSkin != null)
            {
                if (currentSkin.GetAddonModule() != null)
                    currentSkin.GetAddonModule().Disable(objectInScene);
            }
            currentSkin = skin;
            objectInScene.transform.GetChild(0).gameObject.GetComponent<Light>().color = skin.GetColor();
            objectInScene.GetComponent<SpriteRenderer>().color = skin.GetColor();
        }

        public void ResetScore()
        {
            score = 0;
        }
     
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
            float distance = Mathf.Abs(objectInScene.transform.position.x - GameObject.Find("Ball").transform.position.x);
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
            if (playerType == PlayerTypes.pc) playerType = PlayerTypes.player;
            else playerType = PlayerTypes.pc;
        }

        public Player(GameObject objectInScene, PlayerTypes controlMode, Controls controlSetup, int skinID = -1)
        {
            this.objectInScene = objectInScene;
            playerType = controlMode;
            controls = controlSetup;
            if (skinID != -1) ChangeSkin(skinID);
        }
        public Player(GameObject objectInScene, Controls controlSetup, Player other)
        {
            this.objectInScene = objectInScene;
            this.controls = controlSetup;
            this.playerType = other.playerType;
            if (other.GetCurrentSkin() != null) ChangeSkin(other.GetCurrentSkin());

        }
    }
}