﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPrinter : MonoBehaviour
{
    static readonly KeyCode[] keyCodes = { KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2,
    KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7,
    KeyCode.Alpha8, KeyCode.Alpha9 };

    public GameManager gameManager;
    Player player;

    public GameObject itemList;
    public GameObject itemPrinterPrefab;
    public Text description;

    public float timeOut = 0.1f;
    private float timeElapsed;

    int selectedItem = 0;
    public bool IsVisible { get; private set; } = false;

    // Start is called before the first frame update
    void Start() {
        player = gameManager.floor.Player;
        gameManager.bagPrinter = this;
        IsVisible = true;
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
    }

    void OnEnable() {
        IsVisible = true;
        foreach (var child in itemList.GetComponentsInChildren<Text>())
            Destroy(child.gameObject);

        //foreach (var item in player.Items) {
        for (var i = 0; i < player.Items.Count; i++) {
            var itemPrinter = Instantiate(itemPrinterPrefab, itemList.transform);
            var textComponent = itemPrinter.GetComponent<Text>();
            textComponent.text = player.Items[i].ToString();
            if (i == selectedItem) {
                textComponent.color = Color.red;
                description.text = ItemData.GetDescription(player.Items[i]);
            }
        }
    }

    void OnDisable() {
        IsVisible = false;
    }

    // Update is called once per frame
    void Update() {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeOut) {
            timeElapsed = 0.0f;
        }

        ProcessKeyInput();
    }

    void ProcessKeyInput() {
        if (!IsVisible) return;

        for (var i = 0; i < keyCodes.Length; i++)
            if (Input.GetKeyUp(keyCodes[i]))
                SelectItem(i);

        if (Input.GetKeyUp(KeyCode.U)) {
            Use();
        }
        if (Input.GetKeyUp(KeyCode.E)) {
            Equip();
        }
        if (Input.GetKeyUp(KeyCode.T)) {
            Throw();
        }
        if (Input.GetKeyUp(KeyCode.I))
            gameObject.SetActive(false);

    }

    public void Use() {
        player.Use(selectedItem);
        gameObject.SetActive(false);
    }

    public void Throw() {
        player.Throw(selectedItem);
        gameObject.SetActive(false);
    }

    public void Equip() {
        player.Equip(selectedItem);
        gameObject.SetActive(false);
    }

    public void SelectItem(int index) {
        selectedItem = index;
        description.text = ItemData.GetDescription(player.Items[index]);
        OnEnable();
    }
}
