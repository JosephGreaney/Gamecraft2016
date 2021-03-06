﻿using UnityEngine;
using System.Collections;

public class EnemyRabbit : MonoBehaviour {

    private float speed = 1f;
    private float dir = 1f;
    private Rigidbody2D m_Rigidbody2D;

    private bool isFlipping;

	// Use this for initialization
	void Start () {
        isFlipping = false;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(Flip());
    }
	
	// Update is called once per frame
	void Update () {
        m_Rigidbody2D.velocity = new Vector2(dir * speed, m_Rigidbody2D.velocity.y);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Floor" || coll.gameObject.tag == "Enemy")
        {
            dir *= -1;
            StartCoroutine(Flip());
        }
    }

    private IEnumerator Flip()
    {
        if (!isFlipping)
        {
            isFlipping = true;
            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            yield return new WaitForSeconds(0.5f);
            isFlipping = false;
        }
    }
}
