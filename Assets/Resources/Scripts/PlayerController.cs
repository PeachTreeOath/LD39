using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates and manages player board pieces.
/// </summary>
public class PlayerController : Singleton<PlayerController>
{

    public Vector2 startingPosition = new Vector2(4, 4);

    private List<PlayerBoardPiece> players = new List<PlayerBoardPiece>();

    private LifeStatManager lifeStatManager;

    // This is manually called by BoardManager to get ordering correct.
    public void Init()
    {
        lifeStatManager = GameObject.Find("Managers").GetComponent<LifeStatManager>();

        if (BoardManager.instance.relationshipBoardOn)
        {
            PlayerBoardPiece relationshipPlayer = CreatePlayer();
            relationshipPlayer.name = "Relationship Player";
            relationshipPlayer.transform.position = new Vector2(-2.5f, 2.5f);
            relationshipPlayer.SetBoard(BoardManager.instance.relationshipBoard);
            players.Add(relationshipPlayer);
        }
        if (BoardManager.instance.cashBoardOn)
        {
            PlayerBoardPiece cashPlayer = CreatePlayer();
            cashPlayer.name = "Cash Player";
            cashPlayer.transform.position = new Vector2(-2.5f, -2.5f);
            cashPlayer.SetBoard(BoardManager.instance.cashBoard);
            players.Add(cashPlayer);
        }
        if (BoardManager.instance.educationBoardOn)
        {
            PlayerBoardPiece educationPlayer = CreatePlayer();
            educationPlayer.name = "Education Player";
            educationPlayer.transform.position = new Vector2(2.5f, -2.5f);
            educationPlayer.SetBoard(BoardManager.instance.educationBoard);
            players.Add(educationPlayer);
        }
        if (BoardManager.instance.healthBoardOn)
        {
            PlayerBoardPiece healthPlayer = CreatePlayer();
            healthPlayer.name = "Health Player";
            healthPlayer.transform.position = new Vector2(2.5f, 2.5f);
            healthPlayer.SetBoard(BoardManager.instance.healthBoard);
            players.Add(healthPlayer);
        }

        foreach(PlayerBoardPiece player in players)
        {
            player.SetPosition((int)startingPosition.x, (int)startingPosition.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Up"))
        {
            foreach(PlayerBoardPiece player in players)
            {
                player.MoveUp();
            }
            lifeStatManager.age++;
        }
        if (Input.GetButtonDown("Down"))
        {
            foreach (PlayerBoardPiece player in players)
            {
                player.MoveDown();
            }
            lifeStatManager.age++;
        }
        if (Input.GetButtonDown("Left"))
        {
            foreach (PlayerBoardPiece player in players)
            {
                player.MoveLeft();
            }
            lifeStatManager.age++;
        }
        if (Input.GetButtonDown("Right"))
        {
            foreach (PlayerBoardPiece player in players)
            {
                player.MoveRight();
            }
            lifeStatManager.age++;
        }
    }

    private PlayerBoardPiece CreatePlayer()
    {
        PlayerBoardPiece newPlayer = Instantiate(ResourceLoader.instance.playerBoardPieceFab).GetComponent<PlayerBoardPiece>();
        return newPlayer;
    }

    void AdvanceTurn()
    {
        TurnBehaviour[] turnBehaviours = Object.FindObjectsOfType<TurnBehaviour>();
        foreach (TurnBehaviour behaviour in turnBehaviours)
        {
            behaviour.OnAdvanceTurn();
        }
    }
}
