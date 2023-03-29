using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // Externals
    [SerializeField] string itemName;                       // the name of the item to increment
    [SerializeField] GameObject itemSpriteSourcePrefab;     // the prefab that will provide the sprite for the item
    SpriteRenderer itemSprite;
    BuildUI buildUI;


    void Start()
    {
        itemSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        StateManager stateManager = GameObject.FindGameObjectWithTag("StateManager").GetComponent<StateManager>();
        buildUI = (BuildUI)stateManager.GetUI(GameState.Building);

        ItemSprite itemSpriteComponent = itemSpriteSourcePrefab.GetComponent<ItemSprite>();
        if (itemSpriteComponent != null)
        {
            // If the item has a custom sprite, use that
            itemSprite.sprite = itemSpriteComponent.itemSprite;
            itemSprite.color = Color.white;
        }
        else
        {
            // Otherwise use the same sprite it's using in the world
            itemSprite.sprite = itemSpriteSourcePrefab.GetComponent<SpriteRenderer>().sprite;
            itemSprite.color = itemSpriteSourcePrefab.GetComponent<SpriteRenderer>().color;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            buildUI.IncreaseItemAmount(itemName);
            Destroy(gameObject);
        }
    }
}
