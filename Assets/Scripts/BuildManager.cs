using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class InventorySlot
{
    public GameObject itemPrefab;
    [HideInInspector] public Image slotImage;
    [HideInInspector] public Image itemSprite;
    [HideInInspector] public TMP_Text amountText;
    public int amount;
    public string itemName;
}

public class BuildManager : MonoBehaviour
{
    // Externals
    [SerializeField] float buildmodeTimeScale = 0.1f;   // time factor used when in buildmode
    [SerializeField] GameObject previewPrefab;          // prefab that is spawned to used to show build preview
    SpriteRenderer previewSprite;
    Canvas buildmodeCanvas;
    AudioSource audioSource;

    // State
    [SerializeField] List<InventorySlot> inventory;     // items given to the player and their quantities
    int selectedSlot = 0;
    bool buildmodeOn = false;

    // Logic
    void Start()
    {
        // Get externals
        previewSprite = Instantiate(previewPrefab, Vector3.zero, Quaternion.identity).GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        buildmodeCanvas = GetComponent<Canvas>();
        for (int i = 0; i < inventory.Count; i++)
        {
            // Find other external references based on the main one for ease of use in the editor
            inventory[i].slotImage = transform.GetChild(i).GetComponent<Image>();
            inventory[i].itemSprite = inventory[i].slotImage.transform.GetChild(0).GetComponent<Image>();
            inventory[i].amountText = inventory[i].slotImage.transform.GetChild(1).GetComponent<TMP_Text>();

            // Set number in slot to actual number at startup
            inventory[i].amountText.text = inventory[i].amount.ToString();

            // Set image in UI based on prefab
            ItemSprite itemSpriteComponent = inventory[i].itemPrefab.GetComponent<ItemSprite>();
            if (itemSpriteComponent != null)
            {
                // If the item has a custom sprite, use that
                inventory[i].itemSprite.sprite = itemSpriteComponent.itemSprite;
                inventory[i].itemSprite.color = Color.white;
            }
            else
            {
                // Otherwise use the same sprite it's using in the world
                inventory[i].itemSprite.sprite = inventory[i].itemPrefab.GetComponent<SpriteRenderer>().sprite;
                inventory[i].itemSprite.color = inventory[i].itemPrefab.GetComponent<SpriteRenderer>().color;
            }
        }

        // Set initial state
        SelectItem(0);
        SetBuildMode(false);
    }

    void SetBuildMode(bool newBuildmode)
    {
        buildmodeOn = newBuildmode;
        buildmodeCanvas.enabled = buildmodeOn;
        previewSprite.enabled = buildmodeOn;
        Time.timeScale = buildmodeOn ? buildmodeTimeScale : 1f;
    }

    void Update()
    {
        // Toggle between build mode and normal mode
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SetBuildMode(!buildmodeOn);
        }

        // Do no more if build mode is off
        if (!buildmodeOn) return;

        // Hotbar hotkeys
        List<KeyCode> hotbarKeys = new List<KeyCode> { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5 };
        int pressedKey = hotbarKeys.FindIndex(k => Input.GetKeyDown(k));
        if (pressedKey != -1)
        {
            SelectItem(pressedKey);
        }

        // Update item preview
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        previewSprite.transform.position = mousePos; // Move preview to mouse

        // Use a box because regular overlap detection does not work with triggers
        Vector2 overlapBoxPos = Utils.Vec2ToVec3(previewSprite.transform.position);
        Vector2 overlapBoxSize = Utils.Vec2ToVec3(previewSprite.bounds.size);
        int numOverlaps = Physics2D.OverlapBoxAll(overlapBoxPos, overlapBoxSize, 0).Length;

        previewSprite.color = (numOverlaps > 0) ? new Color(255, 0, 0, 0.5f) : new Color(255, 255, 255, 0.5f);

        // Place item
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (numOverlaps == 0 && inventory[selectedSlot].amount > 0)
            {
                Instantiate(inventory[selectedSlot].itemPrefab, mousePos, new Quaternion(0, 0, 0, 0));
                SetSlotAmount(selectedSlot, inventory[selectedSlot].amount - 1);
            }
            else
            {
                audioSource.Play();
            }
        }
    }

    // Interfaces
    public void SelectItem(int selected)
    {
        selectedSlot = selected;
        for (int i = 0; i < inventory.Count; i++)
        {
            InventorySlot slot = inventory[i];
            slot.slotImage.color = (i == selected) ? new Color(0.9f, 0.9f, 0.9f, 1) : new Color(0.8f, 0.8f, 0.8f, 1);
        }

        previewSprite.sprite = inventory[selectedSlot].itemPrefab.GetComponent<SpriteRenderer>().sprite;
        previewSprite.transform.localScale = inventory[selectedSlot].itemPrefab.transform.localScale;
    }

    public void SetSlotAmount(int slot, int newAmount)
    {
        inventory[slot].amount = newAmount;
        inventory[slot].amountText.text = newAmount.ToString();
    }

    public void IncreaseItemAmount(string name) // returns false if no such item exists
    {
        int idx = inventory.FindIndex(s => s.itemName == name);
        if (idx == -1)
        {
            print("Item name could not be found in inventory. Are you sure you wrote it correctly?");
            return;
        }
        SetSlotAmount(idx, inventory[idx].amount + 1);
    }
}
