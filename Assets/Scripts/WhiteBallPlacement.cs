using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WhiteBallPlacement : MonoBehaviour
{
    private bool positionChanged;

    public void Start()
    {
        positionChanged = false;
    }

    public void OnMouseDrag()
    {
        if (GameManager.instance.PlayerCanPlaceWhiteBall)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
            positionChanged = true;    
        }
    }

    public void OnMouseUp()
    {
        if (positionChanged)
        {
            positionChanged = false;
            GameManager.instance.ValidWhiteBallPosition(gameObject);
        }   
    }
}
