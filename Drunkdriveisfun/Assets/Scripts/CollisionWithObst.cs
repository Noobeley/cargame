using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithObst : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "obstacle")
        {
            GameOverScreen.gameOver = true;
        }
    }
}
