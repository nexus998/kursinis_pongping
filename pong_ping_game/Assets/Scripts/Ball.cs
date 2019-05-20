using UnityEngine;
namespace PongPing
{
    public class Ball
    {
        //unity ball particle system.
        public ParticleSystem particles;
        //the velocity of the ball (calculated here)
        private Vector2 velocity;
        public Vector2 GetVelocity() => velocity;
        //needed for unity
        private GameObject ballObject;
        //used to get screen position from world position.
        private Vector3 screenPos;
        //reference to LevelManager. used to end rounds etc
        private LevelManager manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        //detect if the ball is above or below the screen bounds and bounce it off. (by just flipping velocity values)
        private void SetVerticalBoundaries()
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(ballObject.transform.position);
            //up
            if (screenPos.y >= (Screen.height - 21))
            {
                EmitParticles();
                velocity = new Vector2(velocity.x, -velocity.y);
            }
            //down
            if (screenPos.y <= 21)
            {
                EmitParticles();
                velocity = new Vector2(velocity.x, -velocity.y);
            }
        }
        //detect if the ball is touching the edges of the screen. win round accordingly if it is.
        private void SetHorizontalBoundaries()
        {
            screenPos = Camera.main.WorldToScreenPoint(ballObject.transform.position);
            //left
            if (screenPos.x <= 0)
            {
                manager.GetComponent<LevelManager>().EndRound(leftWon: false);
            }
            //right
            if (screenPos.x >= Screen.width)
            {
                manager.GetComponent<LevelManager>().EndRound(leftWon: true);
            }
        }
        //detect if the ball has touched a platform.
        private void PlatformCollision(GameObject[] playerObjects)
        {
            //in this case, i use screenPos to simply get the world position (didnt think i needed a new var)
            screenPos = ballObject.transform.position;
            //compare ball position to platform position and bounce.
            for(int i = 0; i < playerObjects.Length; i++)
            {
                if (screenPos.y <= playerObjects[i].transform.position.y + 1.5f 
                    && screenPos.y >= playerObjects[i].transform.position.y - 2.1f 
                    && screenPos.x <= playerObjects[i].transform.position.x + 0.1f 
                    && screenPos.x >= playerObjects[i].transform.position.x - 0.1f)
                {
                    EmitParticles();
                    velocity = new Vector2(-velocity.x, velocity.y);
                    
                }
            }
        }
        //public method which contains all other collision methods (for simplicity)
        public void ManageCollisions(GameObject[] playerObjects)
        {
            SetHorizontalBoundaries();
            SetVerticalBoundaries();
            PlatformCollision(playerObjects);
        }
        //use unity's particle system to play some predefined particles i set up in unity (looks cool)
        private void EmitParticles()
        {
            //i think it didnt work correctly without stop
            particles.Stop();
            particles.Play();
        }
        //set the ball's velocity to north-west at the force of 10.
        public void LaunchBall()
        {
            velocity = new Vector2(-1, 1) * 10;
        }
        //set ball's velocity to 0
        public void StopBall()
        {
            velocity = Vector2.zero;
        }
        //increase velocity by a custom amount.
        public void IncreaseBallVelocity(float multiplier)
        {
            velocity *= multiplier;
        }
        //constructor
        public Ball(GameObject ballObject, ParticleSystem particleSystem)
        {
            this.ballObject = ballObject;
            this.particles = particleSystem;
        }
    }
}