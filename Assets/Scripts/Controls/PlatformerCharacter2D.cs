using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 600f;                  // Amount of force added when the player jumps.

        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        private bool m_JumpLock;
        private int keys = 0;

        public Text timerText;
        private float timer;
        private int seconds;
        private int minutes;
        private String minString;
        private String secString;

        void Start()
        {
            timer = 0.0f;
            minutes = 0;
            m_JumpLock = false;
        }

        private void Awake()
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }

		public void Move(float move, bool jump)
		{
            // Move the character
            m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
            
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
                
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("PickUp"))
            {
                other.gameObject.SetActive(false);
                keys++;
            }
        }

        void UpdateTimer()
        {
            //deltaTime is the amount of time the last frame took
            timer += Time.deltaTime;
            seconds = (int)timer - (minutes * 60);
            minutes = ((int)timer) / 60;
            if (seconds < 10)
                secString = "0" + (seconds.ToString());
            else
                secString = seconds.ToString();
            if (minutes < 10)
                minString = "0" + (minutes.ToString());
            else
                minString = minutes.ToString();
            //timerText.text = minString + ":" + secString;
        }

        public void Jump(float forceMultiplier)
        {
            if (!m_JumpLock)
            {
                m_JumpLock = true;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce * forceMultiplier));
                StartCoroutine(UnlockJump(0.5f));
            }
        }

        private IEnumerator UnlockJump(float delay)
        {
            yield return new WaitForSeconds(delay);
            m_JumpLock = false;
        }
    }
}
