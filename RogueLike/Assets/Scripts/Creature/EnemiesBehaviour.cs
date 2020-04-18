using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesBehaviour : MonoBehaviour {
    public GameManager gameManager;
    public EnemyBehaviour enemyPrefab;
    List<Enemy> enemies;

    // Start is called before the first frame update
    void Start() {
        enemies = gameManager.floor.Enemies;

        foreach (var enemy in enemies) {
            Instantiate(enemyPrefab, transform).enemy = enemy;
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}