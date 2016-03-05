using UnityEngine;
using System.Collections;

public class Ring : MonoBehaviour {

    //setting the target and movement speed
    public Transform target;
    public float speed = 2f;
    GameObject player;
    private bool m_FacingRight = true;
    private Rigidbody2D m_Rigidbody2D;

    // Use this for initialization
    void Start () {
        //Finding position and target
        player = GameObject.FindGameObjectWithTag("Player");
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        player = GameObject.FindGameObjectWithTag("Player");
        float dir = 0;
        Vector3 relativePoint = transform.InverseTransformPoint(player.transform.position);
        //Check if player is left or right of the enemy

        if (relativePoint.x < 0.0)
        {
            //right
            dir = -1;
        }
        else if (relativePoint.x > 0.0)
        {
            //left
            dir = 1;
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > 0 && distance < 20) {
            transform.LookAt(player.transform);

            m_Rigidbody2D.velocity = new Vector2(dir * speed, m_Rigidbody2D.velocity.y);
        }
        // Set the players direction
        
        //Sets the sprite to be at a set rotation
        transform.rotation = Quaternion.Euler(0, 0, 0);

       

    }
}
