using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BagPrinter : MonoBehaviour
{
    static readonly KeyCode[] keyCodes = { KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2,
    KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7,
    KeyCode.Alpha8, KeyCode.Alpha9 };

    public GameManager gameManager;
    public TapController tapController;
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

        if (player is null) return;
        for (var i = 0; i < player.Items.Count; i++) {
            var itemPrinter = Instantiate(itemPrinterPrefab, itemList.transform);
            itemPrinter.name = i.ToString();
            itemPrinter.AddComponent<EventTrigger>();
            EventTrigger trigger = itemPrinter.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            var index = i;
            entry.callback.AddListener((eventDate) => { SelectItem(index); });
            trigger.triggers.Add(entry);

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

        if (Input.GetKeyUp(KeyCode.W) && selectedItem > 0)
            SelectItem(selectedItem - 1);

        if (Input.GetKeyUp(KeyCode.X) && selectedItem < player.Items.Count - 1)
            SelectItem(selectedItem + 1);

        if (Input.GetKeyUp(KeyCode.U))
            Use();

        if (Input.GetKeyUp(KeyCode.E))
            Equip();

        if (Input.GetKeyUp(KeyCode.T))
            Throw();

        if (Input.GetKeyUp(KeyCode.I))
            gameObject.SetActive(false);

    }

    public void Use() {
        player.Use(selectedItem);
        gameObject.SetActive(false);
        tapController.gameObject.SetActive(true);
    }

    public void Use(int index) {
        player.Use(index);
        gameObject.SetActive(false);
        tapController.gameObject.SetActive(true);
    }

    public void Throw() {
        player.Throw(selectedItem);
        gameObject.SetActive(false);
        tapController.gameObject.SetActive(true);
    }

    public void Equip() {
        player.Equip(selectedItem);
        gameObject.SetActive(false);
        tapController.gameObject.SetActive(true);
    }

    public void SelectItem(int index) {
        selectedItem = index;
        description.text = ItemData.GetDescription(player.Items[index]);
        OnEnable();
    }
}
