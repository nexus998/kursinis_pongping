using UnityEngine;

namespace PongPing
{
    public class Ball : MonoBehaviour
    {

        private void SetVerticalBoundaries()
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            if (screenPos.y >= Screen.height)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -GetComponent<Rigidbody2D>().velocity.y);
            }
            if (screenPos.y <= 0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        private void SetHorizontalBoundaries()
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            if (screenPos.x <= 0)
            {
                //GetComponent<Rigidbody2D>().velocity = new Vector2(-GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
                GameObject.Find("LevelManager").GetComponent<LevelManager>().EndRound(leftWon: false);
            }
            if (screenPos.x >= Screen.width)
            {
                GameObject.Find("LevelManager").GetComponent<LevelManager>().EndRound(leftWon: true);
                //GetComponent<Rigidbody2D>().velocity = new Vector2(-GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
            }
        }
        private void FixedUpdate()
        {
            SetVerticalBoundaries();
            SetHorizontalBoundaries();
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}