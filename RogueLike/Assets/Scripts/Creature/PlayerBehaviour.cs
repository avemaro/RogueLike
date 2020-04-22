using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
    public GameManager gameManager;
    Player player;

    [SerializeField] int Level;
    [SerializeField] int Exp;
    [SerializeField] string HP;
    [SerializeField] int BasicAP;
    [SerializeField] int AP;
    [SerializeField] int DP;
    [SerializeField] string Satiation;
    [SerializeField] string Position;

    //public GameObject attackUp;
    //public GameObject attackUpRight;
    //public GameObject attackRight;
    //public GameObject attackDownRight;
    //public GameObject attackDown;
    //public GameObject attackDownLeft;
    //public GameObject attackLeft;
    //public GameObject attackUpLeft;

    public float timeOut = 0.1f;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start() {
        player = gameManager.floor.Player;
        gameManager.playerBehaviour = this;
    }

    // Update is called once per frame
    void Update() {
        Level = player.Level;
        Exp = player.Exp;
        HP = player.HP.ToString() + " / " + player.MaxHP.ToString();
        BasicAP = player.BasicAP;
        AP = player.AP;
        DP = player.DP;
        Satiation = player.Satiation.ToString() + " / " + player.MaxSatiation.ToString();
        Position = "x:" + player.Position.x + ", y:" + player.Position.y;

        timeElapsed += Time.deltaTime;
        if (timeElapsed >= timeOut) {
            foreach (Transform child in transform)
                child.gameObject.SetActive(false);

            timeElapsed = 0.0f;
        }
    }

    public void Move(Direction direction) {
        player.Move(direction);
    }

    public void Attack() {
        player.Attack();
        //switch (player.direction) {
        //    case Direction.up: attackUp.SetActive(true); return;
        //    case Direction.upRight: attackUpRight.SetActive(true); return;
        //    case Direction.right: attackRight.SetActive(true); return;
        //    case Direction.downRight: attackDownRight.SetActive(true); return;
        //    case Direction.down: attackDown.SetActive(true); return;
        //    case Direction.downLeft: attackDownLeft.SetActive(true); return;
        //    case Direction.left: attackLeft.SetActive(true); return;
        //    case Direction.upLeft: attackUpLeft.SetActive(true); return;
        //    default: return;
        //}
    }

    public void Spawn(Chess piece) {

    }
}
