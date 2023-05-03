using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEnterer : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            GameManager.instance.EnterBall(collision.gameObject.GetComponent<BallBehaviour>().Ball);
            if(collision.gameObject.GetComponent<BallBehaviour>().Ball.Type != BallType.White)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
