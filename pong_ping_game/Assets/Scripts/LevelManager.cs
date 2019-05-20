using UnityEngine;
using UnityEngine.UI;
namespace PongPing
{
    public class LevelManager : MonoBehaviour
    {
        private int secondsPlaying = 0;
        public int GetSecondsPlayed() => secondsPlaying;

        private bool roundEnded = true;
        private string winnerSide = "";
        public int Rounds { get; set; } = 11;

        private Player[] players = new Player[2];
        public GameObject[] playerObjects;

        public GameObject ballObject;
        private Ball ball;

        private UIManager ui;

        //Instantiate all skins
        private void Awake()
        {
            SkinSelector selector = new SkinSelector();
            selector.AddNewSkins();
        }
        //Instantiate control setups, UI, players and the ball.
        private void Start()
        {
            Controls controls1 = new Controls(KeyCode.W, KeyCode.S);
            Controls controls2 = new Controls(KeyCode.UpArrow, KeyCode.DownArrow);
            ui = new UIManager();
            try
            {
                players[0] = new Player(playerObjects[0], Player.PlayerTypes.player, controls1);
                players[1] = new Player(playerObjects[1], controls2, players[0]);
                ball = new Ball(ballObject, ballObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>());
            }
            catch (UnassignedReferenceException)
            {
                Debug.LogError("Either player objects or ball object was not added in the Unity inspector. Please make sure they are added.");
            }
            catch (System.NullReferenceException)
            {
                Debug.LogError("Well, something went really wrong.");
            }


            //just lazy bug-fixing. added these so that when u first start the game all the UI will be updated.
            ChangeSkinForLeftPlayer();
            ChangeSkinForRightPlayer();
            ToggleControlModeForLeftPlayer();
            ToggleControlModeForRightPlayer();
        }
        private void Update()
        {
            //if round has ended, space can be pressed to start a new one.
            if (roundEnded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartNewRound();
                    roundEnded = false;
                    HideUI();
                }
            }
            //move the ball according to its velocity, which is calculated in Ball.cs
            ballObject.transform.Translate(ball.GetVelocity() * Time.deltaTime);
            //enable collisions for the ball, which are calculated in Ball.cs
            ball.ManageCollisions(playerObjects);
            //enable movement for the platforms, whether with keyboard or via AI. Also if current skin has an addon, activate it.
            for(int i = 0; i < 2; i++)
            {
                if (players[i].GetControlMode() == Player.PlayerTypes.player.ToString()) players[i].PlayerMovement();
                if (players[i].GetControlMode() == Player.PlayerTypes.pc.ToString()) players[i].AIMovement();
                if (players[i].GetCurrentSkin() != null && players[i].GetCurrentSkin().GetAddonModule() != null)
                {
                    players[i].GetCurrentSkin().GetAddonModule().Activate(playerObjects[i]);
                }
            }
        }
        //increase played time by 1 sec.
        private void IncreaseTime()
        {
            secondsPlaying += 1;
        }
        //launch the ball, start slowly increasing its velocity, counting time, update max rounds and current points.
        private void StartNewRound()
        {
            if (roundEnded)
            {
                ball.LaunchBall();
                InvokeRepeating("IncreaseTime", 1, 1);
                InvokeRepeating("IncreaseBallVelocity", 5, 5);
                Rounds = int.Parse(GameObject.Find("Canvas").transform.Find("ui_holder").Find("max_rounds_input").gameObject.GetComponent<InputField>().text);
                ui.DisplayLeftScore(players[0].GetScore().ToString());
                ui.DisplayRightScore(players[1].GetScore().ToString());
            }
        }
        //check if round was last, if was then show initial UI, reset scores and display which side won (also show high scores).
        //if not, determine which side won that round and add score correspondingly.
        //also, set ball's velocity to 0 and position it back to the middle.
        public void EndRound(bool leftWon, bool finalEnd=false)
        {
            if (!finalEnd)
            {
                if (leftWon)
                {
                    players[0].UpScore();
                    ui.DisplayLeftScore(players[0].GetScore().ToString());
                }
                else
                {
                    players[1].UpScore();
                    ui.DisplayRightScore(players[1].GetScore().ToString());
                }
                CheckForWin();
            }
            else
            {
                ShowUI();
                ShowWinnerText();
                ResetScores();
                ui.DisplayLeftHighScore(players[0].GetHighScore().ToString());
                ui.DisplayRightHighScore(players[1].GetHighScore().ToString());
            }
            ball.StopBall();
            ballObject.transform.position = new Vector3(0, 0, 1);
            //allow the players to press space and begin another round.
            roundEnded = true;
            //stop counting time and increasing velocity of the ball.
            CancelInvoke();
        }
        //after match is done, reset all current scores.
        private void ResetScores()
        {
            for(int i = 0; i < players.Length; i++)
            {
                players[i].ResetScore();
            }
            
        }
        //increase the ball's velocity by 10%
        private void IncreaseBallVelocity()
        {
            ball.IncreaseBallVelocity(1.1f);
        }
        //check if that was the last round played and determine which side won the match.
        private void CheckForWin()
        {
            for(int i = 0; i < players.Length; i++)
            {
                if(players[i].GetScore() == Rounds)
                {
                    if (i == 0) winnerSide = "Left";
                    else winnerSide = "Right";
                    EndRound(false, true);
                }
            }
        }

        //unity-style methods for displaying UI elements on screen using Unity UI.
        //also ability to change skins for each player and control modes.
        //FOR UI STUFF------------------------
        #region UnityUI
        public void ChangeSkinForLeftPlayer()
        {
            players[0].ChangeSkin(Random.Range(0, SkinSelector.skins.Count));
            GameObject.Find("skin_left").GetComponent<Text>().text = players[0].GetCurrentSkin().GetName();
        }
        public void ToggleControlModeForLeftPlayer()
        {
            players[0].SwitchControlMode();
            GameObject.Find("type_left").transform.GetChild(0).gameObject.GetComponent<Text>().text = "Type: " + players[0].GetControlMode();
        }

        public void ChangeSkinForRightPlayer()
        {
            players[1].ChangeSkin(Random.Range(0, SkinSelector.skins.Count));
            GameObject.Find("skin_right").GetComponent<Text>().text = players[1].GetCurrentSkin().GetName();
        }
        public void ToggleControlModeForRightPlayer()
        {
            players[1].SwitchControlMode();
            GameObject.Find("type_right").transform.GetChild(0).gameObject.GetComponent<Text>().text = "Type: " + players[1].GetControlMode();
        }

        private void HideUI()
        {
            GameObject.Find("Canvas").transform.Find("ui_holder").gameObject.SetActive(false);
        }
        private void ShowUI()
        {
            GameObject.Find("Canvas").transform.Find("ui_holder").gameObject.SetActive(true);
        }
        private void ShowWinnerText()
        {
            var t = GameObject.Find("Canvas").transform.Find("ui_holder").Find("winner_text").gameObject.GetComponent<Text>();
            t.text = winnerSide + " won!";
            t.gameObject.SetActive(true);
        }
        #endregion
        //------------------------------------
    }
}