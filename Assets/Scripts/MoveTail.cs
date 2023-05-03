using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTail : MonoBehaviour
{
    Vector3 position;
    Rigidbody2D rb;

    public void Start()
    {
        position = transform.localPosition;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void SetForce(float coef)
    {
        if (!GameManager.instance.TurnIsRunning)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            rb.AddForce(transform.right * 15000 * coef);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.localPosition = position;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
