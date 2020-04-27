using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapController : MonoBehaviour
{
    Text text;
    public GameManager gameManager;
    PlayerBehaviour playerBehaviour;
    GameObject bagObject;

    int indexOfTapDown = -1;

    Vector2 center;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        playerBehaviour = gameManager.playerBehaviour;
        bagObject = gameManager.bagPrinter.gameObject;

        center = - gameManager.floorPrinter.transform.localPosition;
        center.y += 1334 / 2;
    }

    // Update is called once per frame
    void Update() {
        //Debug.Log(center);

        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                // タッチ開始
            } else if (touch.phase == TouchPhase.Moved) {
                // タッチ移動
            } else if (touch.phase == TouchPhase.Ended) {
                // タッチ終了
                text.text = touch.position.ToString() + "\n";

                var x = touch.position.x - center.x;
                var y = touch.position.y - center.y;
                var angle = Mathf.Rad2Deg * Mathf.Atan2(y, x);
                var r = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));

                text.text += angle + "\n";
                text.text += r;

                if (r < 100) {
                    playerBehaviour.Attack();
                    return;
                }

                var direction = Direction.up;
                if (angle >= 0 && angle < 22.5) direction = Direction.right;
                if (angle >= 22.5 && angle < 67.5) direction = Direction.upRight;
                if (angle >= 67.5 && angle < 112.5) direction = Direction.up;
                if (angle >= 112.5 && angle < 157.5) direction = Direction.upLeft;
                if (angle >= 157.5 && angle < 180) direction = Direction.left;
                if (angle >= -180 && angle < -157.5) direction = Direction.left;
                if (angle >= -157.5 && angle < -112.5) direction = Direction.downLeft;
                if (angle >= -112.5 && angle < -67.5) direction = Direction.down;
                if (angle >= -67.5 && angle < -22.5) direction = Direction.downRight;
                if (angle >= -22.5 && angle < 0) direction = Direction.right;
                playerBehaviour.Move(direction);
            }
        }
    }

    public void Tap(int index) {
        if (index == 8) {
            playerBehaviour.Attack();
            return;
        }

        var direction = DirectionExtend.GetDirection(index);
        playerBehaviour.Move(direction);
    }

    public void TapDown(int index) {
        indexOfTapDown = index;
    }

    public void TapUp(int index) {
        indexOfTapDown = -1;
    }

    public void Enter(int index) {
        if (indexOfTapDown != 8) return;
        playerBehaviour.Look(DirectionExtend.GetDirection(index));
    }


    public void TapItem() {
        gameManager.DismissMap();
        if (bagObject.activeInHierarchy) {
            bagObject.SetActive(false);
            gameManager.SetFloorActive(true);
        }
        else {
            bagObject.SetActive(true);
            gameManager.SetFloorActive(false);
        }
    }
    public void TapMap() {
        bagObject.SetActive(false);
        gameManager.ToggleMap();
    }
}
