using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // Externals
    [SerializeField] string itemName;                       // the name of the item to increment
    [SerializeField] GameObject itemSpriteSourcePrefab;     // the prefab that will provide the sprite for the item
    SpriteRenderer itemSprite;
    BuildManager buildManager;


    void Start()
    {
        itemSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        buildManager = GameObject.FindGameObjectWithTag("BuildModeUI").GetComponent<BuildManager>();

        itemSprite.sprite = itemSpriteSourcePrefab.GetComponent<SpriteRenderer>().sprite;
        itemSprite.color = itemSpriteSourcePrefab.GetComponent<SpriteRenderer>().color;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            buildManager.IncreaseItemAmount(itemName);
            Destroy(gameObject);
        }
    }
}
