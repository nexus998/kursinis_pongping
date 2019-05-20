using UnityEngine;
namespace PongPing
{
    public class LevelManager : MonoBehaviour
    {
        private int secondsPlaying = 0;
        public int GetSecondsPlayed() => secondsPlaying;
        private bool roundEnded = true;
        private Player[] players;
        public GameObject[] playerObjects;
        public GameObject ballObject;
        private UIManager ui;
        private Ball ball;

        private void Start()
        {
            Controls controls1 = new Controls(KeyCode.W, KeyCode.S);
            Controls controls2 = new Controls(KeyCode.UpArrow, KeyCode.DownArrow);
            ui = new UIManager();
            players = new Player[2] { new Player(playerObjects[0], Player.playerTypes.player, controls1), new Player(playerObjects[1], Player.playerTypes.pc, controls2) };
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
                }
            }
            ballObject.transform.Translate(ball.GetVelocity() * Time.deltaTime);
            ball.ManageCollisions(playerObjects);
            for(int i = 0; i < 2; i++)
            {
                if (players[i].GetControlMode() == Player.playerTypes.player.ToString()) players[i].PlayerMovement();
                if (players[i].GetControlMode() == Player.playerTypes.pc.ToString()) players[i].AIMovement();
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
            }
        }
        public void EndRound(bool leftWon)
        {
            if(leftWon)
            {
                players[0].UpScore();
                ui.DisplayLeftScore(players[0].GetScore().ToString());
            }
            else
            {
                players[1].UpScore();
                ui.DisplayRightScore(players[1].GetScore().ToString());
            }
            ball.StopBall();
            ballObject.transform.position = new Vector3(0, 0, 1);
            roundEnded = true;
            CancelInvoke();
        }

        private void IncreaseBallVelocity()
        {
            ball.IncreaseBallVelocity(1.1f);
        }
    }
}