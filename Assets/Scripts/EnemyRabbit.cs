using UnityEngine;
using System.Collections;

public class EnemyRabbit : MonoBehaviour {

    private float speed = 1f;
    private float dir = 1f;
    private Rigidbody2D m_Rigidbody2D;

	// Use this for initialization
	void Start () {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        Flip();
    }
	
	// Update is called once per frame
	void Update () {
        m_Rigidbody2D.velocity = new Vector2(dir * speed, m_Rigidbody2D.velocity.y);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag != "Floor")
            dir *= -1;
            Flip();
    }

    private void Flip()
    {

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
