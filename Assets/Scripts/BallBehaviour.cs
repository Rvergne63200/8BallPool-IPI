using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{

    [SerializeField]
    private Ball _ball;
    
    public Ball Ball
    {
        get
        {
            return _ball;
        }
        private set
        {
            _ball = value;
        }
    }

    public void OnDestroy()
    {
        GameManager.instance.UpdateBallOnTable();
    }

    void Awake()
    {
        gameObject.tag = "Ball";
    }
}

