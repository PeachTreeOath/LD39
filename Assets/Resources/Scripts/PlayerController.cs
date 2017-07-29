using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates and manages player board pieces.
/// </summary>
public class PlayerController : MonoBehaviour
{

    private List<PlayerBoardPiece> players = new List<PlayerBoardPiece>();

    // Use this for initialization
    void Start()
    {
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
        }
        if (Input.GetButtonDown("Down"))
        {
            foreach (PlayerBoardPiece player in players)
            {
                player.MoveDown();
            }
        }
        if (Input.GetButtonDown("Up"))
        {
            foreach (PlayerBoardPiece player in players)
            {
                player.MoveLeft();
            }
        }
        if (Input.GetButtonDown("Up"))
        {
            foreach (PlayerBoardPiece player in players)
            {
                player.MoveRight();
            }
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
