using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPrinter : MonoBehaviour
{
    public GameManager gameManager;
    Player player;

    Text text;
    int selectedItem = -1;

    // Start is called before the first frame update
    void Start() {
        text = gameObject.GetComponent<Text>();
        player = gameManager.floor.Player;
        gameManager.bagPrinter = this;
    }

    // Update is called once per frame
    void Update() {
        text.text = "";
        foreach (var item in player.Items) {
            text.text += item.ToString();
            text.text += "\n";
        }
    }

    public void Use() {
        if (selectedItem == -1) return;
        player.Use(selectedItem);
        selectedItem = -1;
    }

    public void Throw() {
        if (selectedItem == -1) return;
        player.Throw(selectedItem);
        selectedItem = -1;
    }

    public void Equip() {
        if (selectedItem == -1) return;
        player.Equip(selectedItem);
        selectedItem = -1;
    }

    public void SelectItem(int index) {
        selectedItem = index;
    }
}
