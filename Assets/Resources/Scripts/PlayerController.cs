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

    private List<BoardPiece> turnPickups = new List<BoardPiece>();
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

        foreach (PlayerBoardPiece player in players)
        {
            player.SetPosition((int)startingPosition.x, (int)startingPosition.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool turnWillAdv = false;
        if (Input.GetButtonDown("Up"))
        {
            foreach (PlayerBoardPiece player in players)
            {
                player.MoveUp();
                turnPickups.AddRange(player.GetPickups());
            }
            turnWillAdv = true;
        }
        else if (Input.GetButtonDown("Down"))
        {
            foreach (PlayerBoardPiece player in players)
            {
                player.MoveDown();
                turnPickups.AddRange(player.GetPickups());
            }
            turnWillAdv = true;
        }
        else if (Input.GetButtonDown("Left"))
        {
            foreach (PlayerBoardPiece player in players)
            {
                player.MoveLeft();
                turnPickups.AddRange(player.GetPickups());
            }
            turnWillAdv = true;
        }
        else if (Input.GetButtonDown("Right"))
        {
            foreach (PlayerBoardPiece player in players)
            {
                player.MoveRight();
                turnPickups.AddRange(player.GetPickups());
            }
            turnWillAdv = true;
        }

        processPickups(turnPickups);

        if (turnWillAdv)
        {
            AdvanceTurn();
        }
        turnPickups.Clear();
    }

    private void processPickups(List<BoardPiece> pickups)
    {
        foreach (BoardPiece bp in pickups)
        {
            bp.OnPickup();

            System.Type bpType = bp.GetType();
            if (bpType == typeof(PotionBoardPiece))
            {
                LogManager.instance.Log("Potion picked up.");
                AudioManager.instance.PlaySound("PotionPickup");
            }
            else if (bpType == typeof(LootBoardPiece))
            {
                LogManager.instance.Log("Cash me outside.");
                AudioManager.instance.PlaySound("MoneyPickup");
            }
            else if (bpType == typeof(EduBookBoardPiece))
            {
                LogManager.instance.Log("Book picked up.");
                LifeStatManager.instance.addBooks(((EduBookBoardPiece)bp).bookValue);
                AudioManager.instance.PlaySound("BookPickup");
            }
            else if (bpType == typeof(WaifuBoardPiece))
            {
                LogManager.instance.Log("She said yes!");
                AudioManager.instance.PlaySound("Marriage");
                WaifuPortraitSwapper.instance.TurnOnPortrait((WaifuBoardPiece)bp);
            }
            else if (bpType == typeof(ZoneKeyBoardPiece))
            {
                Debug.Log("Keys to the city, baby");
                LogManager.instance.Log("Keys to the city, baby");
            }
        }
    }

    private PlayerBoardPiece CreatePlayer()
    {
        PlayerBoardPiece newPlayer = Instantiate(ResourceLoader.instance.playerBoardPieceFab).GetComponent<PlayerBoardPiece>();
        return newPlayer;
    }
    TurnBehaviour[] turnBehaviours;
    void AdvanceTurn()
    {
        //Handle any local logic
        //Advance stats first so behaviours will get correct data
        lifeStatManager.age++;

        //Invoke listeners that implement TurnBehaviour
        if (turnBehaviours == null)
        {
            turnBehaviours = FindObjectsOfType<TurnBehaviour>();
        }
        foreach (TurnBehaviour behaviour in turnBehaviours)
        {
            behaviour.OnAdvanceTurn();
        }
    }
}
