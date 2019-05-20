using UnityEngine;
namespace PongPing
{
    public class Ball
    {
        public ParticleSystem particles;
        private Vector2 velocity;
        private GameObject ballObject;
        private void SetVerticalBoundaries()
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(ballObject.transform.position);
            if (screenPos.y >= (Screen.height - 15))
            {
                velocity = new Vector2(velocity.x, -velocity.y);
                EmitParticles();

            }
            if (screenPos.y <= 15)
            {
                velocity = new Vector2(velocity.x, -velocity.y);
                EmitParticles();
            }
        }
        private void SetHorizontalBoundaries()
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(ballObject.transform.position);
            if (screenPos.x <= 0)
            {
                GameObject.Find("LevelManager").GetComponent<LevelManager>().EndRound(leftWon: false);
            }
            if (screenPos.x >= Screen.width)
            {
                GameObject.Find("LevelManager").GetComponent<LevelManager>().EndRound(leftWon: true);
            }
        }
        private void PlatformCollision()
        {
            velocity = new Vector2(-velocity.x, velocity.y);
            EmitParticles();
        }

        public void ManageCollisions()
        {
            SetHorizontalBoundaries();
            SetVerticalBoundaries();
            PlatformCollision();
        }

        private void EmitParticles()
        {
            particles.Stop();
            particles.Play();
        }

        public Ball(Vector2 velocity, GameObject ballObject, ParticleSystem particleSystem)
        {
            this.velocity = velocity;
            this.ballObject = ballObject;
            this.particles = particleSystem;
        }
    }
}