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
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        //private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        //private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        private bool m_FacingRight = true;  // For determining which way the player is currently facing.
        

        private int score = 0;
        public Text scoreText;
        public Text timerText;
        private float timer;
        private int seconds;
        private int minutes;
        private String minString;
        private String secString;

        void Start()
        {
        }

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            //m_CeilingCheck = transform.Find("CeilingCheck");
            //m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }


        private void FixedUpdate()
        {
            m_Grounded = false;
            // This is used to check if the feet of the player are touching ground.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                    m_Grounded = true;
            }

            //m_Anim.SetBool("Ground", m_Grounded);
            m_Rigidbody2D.rotation = 0;
            // Set the vertical animation
            //m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
            //UpdateTimer();

        }

		public void Move(float move, bool jump)
		{
			// Check if the vertical speed will be below the limit
            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
            }
            // If the player should jump...
            if (m_Grounded && jump) // && m_Anim.GetBool("Ground")
            {
                // Add a vertical force to the player.
                m_Grounded = false;
                //m_Anim.SetBool("Ground", false);
			    m_Rigidbody2D.AddForce (new Vector2 (0f, m_JumpForce));
            }
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


        /*void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("PickUp"))
            {
                other.gameObject.SetActive(false);
                score++;
                Debug.Log(score);
                UpdateScoreText();
            }
        }*/

        void UpdateScoreText()
        {
            scoreText.text = "Score: " + score.ToString();
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
            timerText.text = minString + ":" + secString;
        }
    }
}