using UnityEngine;
namespace PongPing
{
    public class Ball
    {
        public ParticleSystem particles;
        private Vector2 velocity;
        public Vector2 GetVelocity() => velocity;
        private GameObject ballObject;
        private Vector3 screenPos;

        private LevelManager manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        private void SetVerticalBoundaries()
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(ballObject.transform.position);
            if (screenPos.y >= (Screen.height - 21))
            {
                velocity = new Vector2(velocity.x, -velocity.y);
                EmitParticles();

            }
            if (screenPos.y <= 21)
            {
                velocity = new Vector2(velocity.x, -velocity.y);
                EmitParticles();
            }
        }
        private void SetHorizontalBoundaries()
        {
            screenPos = Camera.main.WorldToScreenPoint(ballObject.transform.position);
            if (screenPos.x <= 0)
            {
                manager.GetComponent<LevelManager>().EndRound(leftWon: false);
            }
            if (screenPos.x >= Screen.width)
            {
                manager.GetComponent<LevelManager>().EndRound(leftWon: true);
            }
        }
        private void PlatformCollision(GameObject[] playerObjects)
        {
            screenPos = ballObject.transform.position;

            for(int i = 0; i < playerObjects.Length; i++)
            {
                if (screenPos.y <= playerObjects[i].transform.position.y + 1.5f 
                    && screenPos.y >= playerObjects[i].transform.position.y - 1.95f 
                    && screenPos.x <= playerObjects[i].transform.position.x + 0.1f 
                    && screenPos.x >= playerObjects[i].transform.position.x - 0.1f)
                {
                    velocity = new Vector2(-velocity.x, velocity.y);
                    EmitParticles();
                }
            }
        }

        public void ManageCollisions(GameObject[] playerObjects)
        {
            SetHorizontalBoundaries();
            SetVerticalBoundaries();
            PlatformCollision(playerObjects);
        }

        private void EmitParticles()
        {
            particles.Stop();
            particles.Play();
        }
        
        public void LaunchBall()
        {
            velocity = new Vector2(-1, 1) * 10;
        }
        public void StopBall()
        {
            velocity = Vector2.zero;
        }
        public void IncreaseBallVelocity(float multiplier)
        {
            velocity *= multiplier;
        }

        public Ball(GameObject ballObject, ParticleSystem particleSystem)
        {
            this.ballObject = ballObject;
            this.particles = particleSystem;
        }
    }
}