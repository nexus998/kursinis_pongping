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
        private Player[] players;
        public GameObject[] playerObjects;
        public GameObject ballObject;
        private UIManager ui;
        private Ball ball;
        public int Rounds { get; set; } = 11;

        private void Awake()
        {
            SkinSelector selector = new SkinSelector();
            selector.AddNewSkins();
        }
        private void Start()
        {
            Controls controls1 = new Controls(KeyCode.W, KeyCode.S);
            Controls controls2 = new Controls(KeyCode.UpArrow, KeyCode.DownArrow);
            ui = new UIManager();
            players = new Player[2] { new Player(playerObjects[0], Player.PlayerTypes.player, controls1), new Player(playerObjects[1], Player.PlayerTypes.pc, controls2, 0) };
            ball = new Ball(ballObject, ballObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>());
        }
        private void Update()
        {
            if (roundEnded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartNewRound();
                    roundEnded = false;
                    HideUI();
                }
            }
            ballObject.transform.Translate(ball.GetVelocity() * Time.deltaTime);
            ball.ManageCollisions(playerObjects);
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
        private void IncreaseTime()
        {
            secondsPlaying += 1;
        }
        private void StartNewRound()
        {
            if (roundEnded)
            {
                ball.LaunchBall();
                InvokeRepeating("IncreaseTime", 1, 1);
                InvokeRepeating("IncreaseBallVelocity", 5, 5);
                Rounds = int.Parse(GameObject.Find("Canvas").transform.Find("ui_holder").Find("max_rounds_input").gameObject.GetComponent<InputField>().text);
            }
        }
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
            }
            ball.StopBall();
            ballObject.transform.position = new Vector3(0, 0, 1);
            roundEnded = true;
            CancelInvoke();
        }

        private void ResetScores()
        {
            for(int i = 0; i < players.Length; i++)
            {
                players[i].ResetScore();
            }
            ui.DisplayLeftScore(players[0].GetScore().ToString());
            ui.DisplayRightScore(players[1].GetScore().ToString());
        }

        private void IncreaseBallVelocity()
        {
            ball.IncreaseBallVelocity(1.1f);
        }

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