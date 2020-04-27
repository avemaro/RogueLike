using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
    public GameManager gameManager;
    Player player;

    List<Direction> moveDirections = new List<Direction>();
    bool isAboutToAttack = false; 

    [SerializeField] int Level;
    [SerializeField] int Exp;
    [SerializeField] string HP;
    [SerializeField] int BasicAP;
    [SerializeField] int AP;
    [SerializeField] int DP;
    [SerializeField] string Satiation;
    [SerializeField] string Position;

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
            timeElapsed = 0.0f;

            if (isAboutToAttack) player.Attack();
            isAboutToAttack = false;

            if (moveDirections.Count == 0) return;
            player.Move(moveDirections[0]);
            moveDirections.RemoveAt(0);
        }
    }

    public void Look(Direction direction) {
        player.Look(direction);
    }

    public void Move(Direction direction) {
        if (moveDirections.Count == 0)
            moveDirections.Add(direction);
        else
            moveDirections[0] = direction;
        isAboutToAttack = false;
    }

    public void Attack() {
        isAboutToAttack = true;

        if (moveDirections.Count != 0)
           moveDirections.RemoveAt(0);
    }

    public void Spawn(Chess piece) {

    }
}
