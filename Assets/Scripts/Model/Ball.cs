using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ball
{
    [SerializeField]
    private BallType _type;
    public BallType Type
    {
        get
        {
            return _type;
        }
        private set
        {
            _type = value;
        }
    }
}
