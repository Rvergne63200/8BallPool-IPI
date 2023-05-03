using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    [SerializeField]
    private string _name;

    public string Name 
    {
        get
        {
            return _name;
        }
        private set
        {
            _name = value;
        } 
    }

    [SerializeField]
    private Sprite _playerSprite;
    public Sprite PlayerSprite
    {
        get
        {
            return _playerSprite;
        }
        private set
        {
            _playerSprite = value;
        }
    }


    [SerializeField]
    private BallType _team;
    public BallType Team
    {
        get
        {
            return _team;
        }

        set
        {
            _team = value;
        }
    }
}
