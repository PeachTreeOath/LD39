using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The actual player representation on the board.
/// </summary>
public class PlayerBoardPiece : BoardPiece {

    [SerializeField]
    private Sprite chipForward;
    [SerializeField]
    private Sprite chipBack;
    [SerializeField]
    private Sprite chipLeft;
    [SerializeField]
    private Sprite chipRight;

    protected SpriteRenderer chipSprite;

    void Start() {
        chipSprite = gameObject.GetComponent<SpriteRenderer>();
        if (chipSprite == null) {
            Debug.LogError("Player sprite seems to be missing from PlayerBoardPiece");
        }
        if (chipForward == null) {
            Debug.LogError("Missing forward sprite for player");
        }
        if (chipBack == null) {
            Debug.LogError("Missing back sprite for player");
        }
        if (chipLeft == null) {
            Debug.LogError("Missing left sprite for player");
        }
        if (chipRight == null) {
            Debug.LogError("Missing right sprite for player");
        }
    }

    public override void PostMoveHook(Vector2 from, Vector2 to) {
        base.PostMoveHook(from, to);
        if (chipSprite == null) {
            return; //level may be loading
        }

        //This would reset position to face forward after moving
        //chipSprite.sprite = chipForward;
    }

    public override void PreMoveHook(Vector2 from, Vector2 to) {
        base.PreMoveHook(from, to);
        if (chipSprite == null) {
            return; //level may be loading
        }

        if (to.x - from.x > Mathf.Epsilon) {
            chipSprite.sprite = chipRight;
        } else if (to.x - from.x < -Mathf.Epsilon) {
            chipSprite.sprite = chipLeft;
        } else if (to.y - from.y > Mathf.Epsilon) {
            chipSprite.sprite = chipBack;
        } else {
            chipSprite.sprite = chipForward;
        }
    }

    public void MoveUp() {
        if (CheckValidMove(x, y + 1)) {
            SetPosition(x, y + 1, true);
            //board.PruneFog();
        }
    }
    public void MoveDown() {
        if (CheckValidMove(x, y - 1)) {
            SetPosition(x, y - 1, true);
            //board.PruneFog();
        }
    }
    public void MoveLeft() {
        if (CheckValidMove(x - 1, y)) {
            SetPosition(x - 1, y, true);
            //board.PruneFog();
        }
    }
    public void MoveRight() {
        if (CheckValidMove(x + 1, y)) {
            SetPosition(x + 1, y, true);
        }
    }
    private bool CheckValidMove(int x, int y) {
        return board.IsSquareMovable(x, y);
    }

    public List<BoardPiece> GetPickups() {
        if (isOverPickup()) {
            return getPickups();
        }
        return new List<BoardPiece>();
    }

    public override int GetContentType() {
        return BoardPiece.PLAYER;
    }

}
