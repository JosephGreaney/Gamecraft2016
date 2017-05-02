using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class Floor : MonoBehaviour
{
    [SerializeField]
    private float jumpForceMultiplier = 1;
	
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlatformerCharacter2D player = other.collider.GetComponent<PlatformerCharacter2D>();
        if (player != null)
        {
            player.Jump(jumpForceMultiplier);
        }
    }
}
