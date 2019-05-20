using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PongPing
{
    public class Player
    {
        //speed at which all platforms will move at. units/s
        private const int movementSpeed = 10;

        //score class. keeps current score and high score.
        private ScoreKeeper score = new ScoreKeeper();
        public ScoreKeeper GetScore() => score;
        public int GetHighScore() => GetScore().GetHighScore();
        public void UpScore() { score++; }

        //this is where u choose between ai and player.
        public enum PlayerTypes { player, pc };
        private PlayerTypes playerType;
        public string GetControlMode() => playerType.ToString();

        //distance in units at which AI detect the ball.
        private readonly float detectDistance = 6.5f;

        //class which stores movement method and does not allow to go out of bounds.
        private PlatformController controller = new PlatformController();
        //class which stores movement buttons.
        private Controls controls;

        //required because this is unity. used to get/set location of the platform inside the actual game.
        public GameObject objectInScene;
        
        //skin is a class which stores colors and addons. addons are .cs files attached with skins that can do pretty much anything.
        private Skin currentSkin = null;
        public Skin GetCurrentSkin() => currentSkin;
        //method to change skin with an existing skin inside the SkinSelector class.
        public void ChangeSkin(int skinID)
        {
            if (currentSkin != null)
            {
                //disables any addon that was on the previous skin (if there was)
                if(currentSkin.GetAddonModule() != null)
                    currentSkin.GetAddonModule().Disable(objectInScene);
            }
            //skins is a static list
            SkinSelector selector = new SkinSelector();
            if(SkinSelector.skins.Count >= skinID-1)
            {
                //need to set currentSkin because LevelManager checks the currentSkin and actually activates any addon .cs files.
                currentSkin = SkinSelector.skins[skinID];
                //because skins are basically just color variants, set the color here.
                //sets to the light emitting from the platform and the actual platform (unity spriterenderer color).
                objectInScene.transform.GetChild(0).gameObject.GetComponent<Light>().color = SkinSelector.skins[skinID].GetColor();
                objectInScene.GetComponent<SpriteRenderer>().color = SkinSelector.skins[skinID].GetColor();
            }
            else
            {
                Debug.LogError("Skin with ID of " + skinID + " does not exist!");
            }
        }
        //same as above but does not look for skinID in skin list. could be used for custom skins or programming difficulties.
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

        //a method that calls the method (ScoreKeeper) which resets the player's score to 0.
        public void ResetScore()
        {
            score.ResetScore();
        }
        
        //if playertype is player, this method is used to detect keypresses and call movement functions inside PlatformController.
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
        //if playertype is pc, this method detects x distance from ball to platform and if its lower than the detect distance, moves the platform accordingly,
        //to level with the ball. (very smart)
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
        //used for in-game UI elements i think. switches the current platform between pc and player control modes.
        public void SwitchControlMode()
        {
            if (playerType == PlayerTypes.pc) playerType = PlayerTypes.player;
            else playerType = PlayerTypes.pc;
        }
        //constructor
        public Player(GameObject objectInScene, PlayerTypes controlMode, Controls controlSetup, int skinID = -1)
        {
            this.objectInScene = objectInScene;
            playerType = controlMode;
            controls = controlSetup;
            if (skinID != -1) ChangeSkin(skinID);
        }
        //copy constructor
        public Player(GameObject objectInScene, Controls controlSetup, Player other)
        {
            this.objectInScene = objectInScene;
            playerType = other.playerType;
            controls = controlSetup;
            if (other.GetCurrentSkin() != null) ChangeSkin(other.GetCurrentSkin());
        }
    }
}