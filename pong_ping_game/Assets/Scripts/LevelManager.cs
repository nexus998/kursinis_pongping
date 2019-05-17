using UnityEngine;
namespace PongPing
{
    public class LevelManager : MonoBehaviour
    {
        public int secondsPlaying = 0;
        private bool roundEnded = true;
        public Player[] players;
        private void Start()
        {
            players = new Player[2] {GameObject.Find("Player1").GetComponent<Player>(), GameObject.Find("Player2").GetComponent<Player>()};
            InvokeRepeating("IncreaseTime", 1, 1);
            InvokeRepeating("IncreaseBallVelocity", 5, 5);
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StartNewRound();
            }
        }
        private void IncreaseTime()
        {
            secondsPlaying += 1;
        }
        private void StartNewRound()
        {
            if(roundEnded)GameObject.Find("Ball").GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * 350);
        }
        public void EndRound(bool leftWon)
        {
            if(leftWon) players[0].UpScore();
            else players[1].UpScore();
            GameObject.Find("Ball").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GameObject.Find("Ball").transform.position = new Vector3(0, 0, 1);
            roundEnded = true;
        }

        private void IncreaseBallVelocity()
        {
            GameObject.Find("Ball").GetComponent<Rigidbody2D>().velocity *= 1.1f;
        }
    }
}