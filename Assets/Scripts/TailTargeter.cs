using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailTargeter : MonoBehaviour
{
    public GameObject target;

    public void Start()
    {
        ReplaceTail();
    }

    public void OnMouseDrag()
    {
        if (!GameManager.instance.TurnIsRunning)
        {
            Vector2 myPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPos = transform.position;

            Vector2 toOther = (myPos - targetPos).normalized;

            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(toOther.y, toOther.x) * Mathf.Rad2Deg + 180);
        }
    }

    public void ReplaceTail(bool turnIsRunning = false)
    {
        if(target != null && !turnIsRunning)
        {
            transform.position = target.transform.position;
        }
    }
}
