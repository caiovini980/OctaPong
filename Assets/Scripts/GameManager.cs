using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYER1_TURN, PLAYER2_TURN, WINP1, WINP2 }

public class GameManager : MonoBehaviour
{
    [Tooltip("Current state of the game")]
    public BattleState state;

    [Tooltip("Player 1 prefab")]
    public GameObject player1Prefab;
    [Tooltip("Player 2 prefab")]
    public GameObject player2Prefab;

    [Tooltip("Player 1 start point")]
    public Transform player1StartPosition;
    [Tooltip("Player 2 start point")]
    public Transform player2StartPosition;

    [Tooltip("Amount of time that the game takes to change turns")]
    public int timer = 2;

    private Player1Movement _p1Movement;
    private Player2Movement _p2Movement;
    private Player1Shoot _p1Shoot;
    private Player2Shoot _p2Shoot;
    private PlayerHealth _p1Health, _p2Health;

    private bool _hasPressedSpace = false;
    private bool _hasPressedControl = false;

    private bool _isPlayer1Turn = false;
    private bool _isPlayer2Turn = false;

    private string _PLAYER_1_NAME = "Player1";
    private string _PLAYER_2_NAME = "Player2";

    private string _PLAYER_1_AIM = "Player1Aim";
    private string _PLAYER_2_AIM = "Player2Aim";

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetupGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_p1Health.currentHealth > 0 && _p2Health.currentHealth > 0)
            {
                StartCoroutine(WaitToChangeTurn(timer));
                _hasPressedSpace = true;
            }
        }

        else if (Input.GetKeyDown(KeyCode.RightControl))
        {
            if (_p1Health.currentHealth > 0 && _p2Health.currentHealth > 0)
            {
                StartCoroutine(WaitToChangeTurn(timer));
                _hasPressedControl = true;
            }
        }
    }

    void SetupGame()
    {
        _p1Movement = GameObject.Find(_PLAYER_1_NAME).GetComponent<Player1Movement>();
        _p2Movement = GameObject.Find(_PLAYER_2_NAME).GetComponent<Player2Movement>();

        _p1Shoot = GameObject.Find(_PLAYER_1_AIM).GetComponent<Player1Shoot>();
        _p2Shoot = GameObject.Find(_PLAYER_2_AIM).GetComponent<Player2Shoot>();

        _p1Health = GameObject.Find(_PLAYER_1_NAME).GetComponent<PlayerHealth>();
        _p2Health = GameObject.Find(_PLAYER_2_NAME).GetComponent<PlayerHealth>();

        
        state = BattleState.PLAYER1_TURN;
        Player1Turn();
    }

    void Player1Turn()
    {
        if (state != BattleState.PLAYER1_TURN)
            return;

        _isPlayer1Turn = true;
        _isPlayer2Turn = false;

        _p1Shoot.CanShoot(true);
        _p1Movement.CanWalk(true);

        _p2Shoot.CanShoot(false);
        _p2Movement.CanWalk(false);
    }

    void Player2Turn()
    {
        if (state != BattleState.PLAYER2_TURN)
            return;

        _isPlayer1Turn = false;
        _isPlayer2Turn = true;

        _p1Shoot.CanShoot(false);
        _p1Movement.CanWalk(false);

        _p2Shoot.CanShoot(true);
        _p2Movement.CanWalk(true);
    }

    IEnumerator WaitToChangeTurn(int waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (_isPlayer1Turn)
        {
            _hasPressedControl = false;

            if(_hasPressedSpace)
            {
                _isPlayer1Turn = false;
                _isPlayer2Turn = true;
            
                state = BattleState.PLAYER2_TURN;
                Player2Turn();
            }
        }

        else if (_isPlayer2Turn)
        {
            _hasPressedSpace = false;

            if (_hasPressedControl)
            {
                _isPlayer1Turn = true;
                _isPlayer2Turn = false;

                state = BattleState.PLAYER1_TURN;
                Player1Turn();
            }
        }
    }
}
