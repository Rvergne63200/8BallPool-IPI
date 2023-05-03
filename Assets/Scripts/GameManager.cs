using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField]
    Player[] players;

    [SerializeField]
    GameObject whiteBall;

    [SerializeField]
    Vector2 whiteBallPosition;

    [SerializeField]
    List<Ball> enteredBalls;

    [SerializeField]
    List<Ball> enteredBallsThisTurn;

    [SerializeField]
    TailTargeter playerTargeter;

    [SerializeField]
    UnityEvent<bool> evTurnIsRunning;

    [SerializeField]
    UnityEvent evWhiteBallPositionChanged;

    [SerializeField]
    UnityEvent<Player> evCurrentPlayerChanged;

    [SerializeField]
    UnityEvent<Player> evEndOfGame;

    GameObject[] ballsOnTable;


    private int _currentPlayerId;
    private int CurrentPlayerId {
        get => _currentPlayerId;
        set
        {
            _currentPlayerId = value;
            evCurrentPlayerChanged.Invoke(players[value]);
        }
    }
    private int WaitingPlayerId { get => ((CurrentPlayerId + 1) % 2); }


    public Player currentPlayer { get => players[CurrentPlayerId]; }
    public Player Winner { get; private set; }
    public bool TurnIsRunning { get; private set; }
    public bool PlayerCanPlaceWhiteBall { get; private set; }

    public void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Un GameManager est déjà instancié");
            return;
        }
        
        instance = this;
    }

    public void Start()
    {
        whiteBall = GameObject.Instantiate(whiteBall, whiteBallPosition, Quaternion.identity);
        playerTargeter.target = whiteBall;
            
        enteredBalls = new List<Ball>();
        enteredBallsThisTurn = new List<Ball>();
        ballsOnTable = GameObject.FindGameObjectsWithTag("Ball");
        CurrentPlayerId = 1;
        TurnIsRunning = true;
    }

    public void Update()
    {   
        if (!BallsAreStopped())
        {
            if (!TurnIsRunning)
            {
                TurnIsRunning = true;
                evTurnIsRunning.Invoke(TurnIsRunning);
                PlayerCanPlaceWhiteBall = false;
            }

            return;
        }

        if (TurnIsRunning)
        {
            PlayTurn();
            TurnIsRunning = false;
            evTurnIsRunning.Invoke(TurnIsRunning);
        }
    }


    // Méthode qui gère les règles du jeu
    private void PlayTurn()
    {
        ballsOnTable = GameObject.FindGameObjectsWithTag("Ball");

        bool changePlayer = true;
        bool blackBallIsEnter = false;
        bool whiteBallIsEnter = false;

        if (players[CurrentPlayerId].Team == BallType.White || players[WaitingPlayerId].Team == BallType.White)
        {
            SelectTeam(enteredBallsThisTurn.ToArray());
        }

        foreach (Ball ball in enteredBallsThisTurn)
        {
            switch (ball.Type)
            {
                case BallType.Black:
                    blackBallIsEnter = true;
                    break;

                case BallType.White:
                    whiteBallIsEnter = true;
                    break;

                default:
                    enteredBalls.Add(ball);
                    changePlayer = (ball.Type != players[CurrentPlayerId].Team);
                    break;
            }
        }

        if (blackBallIsEnter)
        {
            EndGame();
        }
        else if (whiteBallIsEnter)
        {
            PlayerCanPlaceWhiteBall = true;
        }

        enteredBallsThisTurn.Clear();
        CurrentPlayerId = changePlayer ? WaitingPlayerId : CurrentPlayerId;


    }

    // Permet d'indiquer qu'une boule vient de rentrer dans un trou
    public void EnterBall(Ball ball)
    {
        enteredBallsThisTurn.Add(ball);
    }


    // Permet de savoir si toutes les boules sont bien arrêtées
    private bool BallsAreStopped()
    {
        bool ballsAreStopped = true;

        foreach (GameObject ball in ballsOnTable)
        {
            if (ball.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            {
                ballsAreStopped = false;
                break;
            }
        }

        return ballsAreStopped;
    }

    // Permet de définir quel joueur doit jouer quel type de boule
    private void SelectTeam(Ball[] enteredBalls)
    {
        foreach(Ball ball in enteredBalls)
        {
            switch (ball.Type)
            {
                case BallType.Plain:
                    players[CurrentPlayerId].Team = ball.Type;
                    players[WaitingPlayerId].Team = BallType.Empty;
                    break;
                case BallType.Empty:
                    players[CurrentPlayerId].Team = ball.Type;
                    players[WaitingPlayerId].Team = BallType.Plain;
                    break;
            }
        }
    }

    // Permet de setup la fin de partie
    private void EndGame()
    {
        if (CountEnteredBall(players[CurrentPlayerId].Team) == 7) 
        {
            Winner = players[CurrentPlayerId];
        }
        else
        {
            Winner = players[WaitingPlayerId];
        }

        evEndOfGame.Invoke(Winner);
    }

    // Renvoie le nombre de boules qui sont rentrées dans les trous
    public int CountEnteredBall(BallType type)
    {
        int number = 0;

        foreach (Ball ball in enteredBalls)
        {
            if (ball.Type == type)
            {
                number += 1;
            }
        }

        return number;
    }

    // Permet de controler le placement de la boule blanche (pour le moment impose de la placer au centre car non finie)
    public void ValidWhiteBallPosition(GameObject whiteBall)
    {
        Vector2 whiteBallPosition = whiteBall.transform.position;
        
        if(whiteBallPosition != this.whiteBallPosition)
        {
            whiteBall.transform.position = this.whiteBallPosition;
        }

        evWhiteBallPositionChanged.Invoke();
    }

    // Permet au manager de savoir quelles sont les boules restantes sur la table de billard
    public void UpdateBallOnTable()
    {
        ballsOnTable = GameObject.FindGameObjectsWithTag("Ball");
    }
}
