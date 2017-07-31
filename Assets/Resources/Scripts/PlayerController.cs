using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Creates and manages player board pieces.
/// </summary>
public class PlayerController : Singleton<PlayerController>
{

    public Vector2 startingPosition = new Vector2(4, 4);
    public bool onDeathScreen = false;
    public bool inputEnabled = true;

    private List<PlayerBoardPiece> players = new List<PlayerBoardPiece>();

    private List<BoardPiece> turnPickups = new List<BoardPiece>();
    private LifeStatManager lifeStatManager;

    // This is manually called by BoardManager to get ordering correct.
    public void Init(List<PlayerBoardPiece> allPlayers)
    {
        lifeStatManager = GameObject.Find("Managers").GetComponent<LifeStatManager>();

        players.Clear();
        players.AddRange(allPlayers);

        if (players.Count == 0) {
            Debug.LogError("No players loaded...are you sure they were in the level data?");
        }

    }

    // Update is called once per frame
    void Update()
    {
        bool turnWillAdv = false;
        if (Input.GetButtonDown("Up") && inputEnabled)
        {
            foreach (PlayerBoardPiece player in players)
            {
                player.MoveUp();
                turnPickups.AddRange(player.GetPickups());
            }
            turnWillAdv = true;
        }
        else if (Input.GetButtonDown("Down") && inputEnabled)
        {
            foreach (PlayerBoardPiece player in players)
            {
                player.MoveDown();
                turnPickups.AddRange(player.GetPickups());
            }
            turnWillAdv = true;
        }
        else if (Input.GetButtonDown("Left") && inputEnabled)
        {
            foreach (PlayerBoardPiece player in players)
            {
                player.MoveLeft();
                turnPickups.AddRange(player.GetPickups());
            }
            turnWillAdv = true;
        }
        else if (Input.GetButtonDown("Right") && inputEnabled)
        {
            foreach (PlayerBoardPiece player in players)
            {
                player.MoveRight();
                turnPickups.AddRange(player.GetPickups());
            }
            turnWillAdv = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && onDeathScreen)
        {
            SceneManager.LoadScene("Game");
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
                LifeStatManager.instance.isMarried = true;
               // WaifuPortraitSwapper.instance.TurnOnPortrait((WaifuBoardPiece)bp);
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
