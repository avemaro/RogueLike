using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    static readonly (KeyCode, Direction)[] keyCodes = {
        (KeyCode.W, Direction.up), (KeyCode.E, Direction.upRight),
        (KeyCode.D, Direction.right), (KeyCode.C, Direction.downRight),
        (KeyCode.X, Direction.down), (KeyCode.Z, Direction.downLeft),
        (KeyCode.A, Direction.left), (KeyCode.Q, Direction.upLeft)
    };

    Floor floor;
    //Player player;
    PlayerBehaviour playerBehaviour;
    public GameManager gameManager;

    KeyCode keyInput;

    public float timeOut　= 0.1f;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        floor = gameManager.floor;
        //player = floor.Player;
        playerBehaviour = gameManager.playerBehaviour;
    }

    void Update() {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeOut) {
            ProcessInput();
            timeElapsed = 0.0f;
        }

        if (Input.GetKeyUp(KeyCode.I))
            gameManager.bagPrinter.gameObject.SetActive(true);
        //gameManager.bagPrinter.ToggleVisible();

        //foreach (var keycode in keyCodes)
        //    if (Input.GetKeyUp(keycode.Item1))
        //        keyInput = keycode.Item1;
    }


    // Update is called once per frame
    void ProcessInput()
    {
        if (gameManager.bagPrinter.IsVisible) return;

        //foreach (var keycode in keyCodes)
        //    if (keycode.Item1 == keyInput)
        //        player.Move(keycode.Item2);
        //keyInput = KeyCode.F1;
        //if (Input.GetKey(keycode.Item1))
        //player.Move(keycode.Item2);

        if (Input.GetKey(KeyCode.W)) {
            playerBehaviour.Move(Direction.up);
            return;
        }
        if (Input.GetKey(KeyCode.E)) {
            playerBehaviour.Move(Direction.upRight);
            return;
        }
        if (Input.GetKey(KeyCode.D)) {
            playerBehaviour.Move(Direction.right);
            return;
        }
        if (Input.GetKey(KeyCode.C)) {
            playerBehaviour.Move(Direction.downRight);
            return;
        }
        if (Input.GetKey(KeyCode.X)) {
            playerBehaviour.Move(Direction.down);
            return;
        }
        if (Input.GetKey(KeyCode.Z)) {
            playerBehaviour.Move(Direction.downLeft);
            return;
        }
        if (Input.GetKey(KeyCode.A)) {
            playerBehaviour.Move(Direction.left);
            return;
        }
        if (Input.GetKey(KeyCode.Q)) {
            playerBehaviour.Move(Direction.upLeft);
            return;
        }

        if (Input.GetKey(KeyCode.S)) {
            playerBehaviour.Attack();
        }
        if (Input.GetKey(KeyCode.M)) {
            gameManager.ShowMap();
        }
        if (Input.GetKey(KeyCode.P)) {
            playerBehaviour.Spawn(Chess.Pawn);
        }
    }
}
