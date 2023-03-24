using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct BuildItem
{
    public GameObject buildableObject;
    public int amount;
}

public class BuildMode : MonoBehaviour
{
    bool _buildModeOn = false;
    [SerializeField] float _buildModeSlowFactor = 0.1f;
    [SerializeField] List<BuildItem> _buildItems;
    [SerializeField] SpriteRenderer phantomSprite;
    int _currentItem = 0;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _buildModeOn = !_buildModeOn;
            Time.timeScale = _buildModeOn ? _buildModeSlowFactor : 1;
            UpdatePhantomSprite();
        }

        if (!_buildModeOn)
        {
            return;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        phantomSprite.transform.position = mousePos;


        Vector2 box2Dpos = new Vector2(phantomSprite.transform.position.x, phantomSprite.transform.position.y);
        Vector2 box2Dscale = new Vector2(phantomSprite.bounds.size.x, phantomSprite.bounds.size.y);
        int numOverlaps = Physics2D.OverlapBoxAll(box2Dpos, box2Dscale, 0).Length;
        bool phantomOverlaps = numOverlaps > 0;


        phantomSprite.color = new Color(255, 255, 255, 0.5f);
        if (phantomOverlaps)
        {
            phantomSprite.color = new Color(255, 0, 0, 0.5f);
            return;
        }


        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(_buildItems[_currentItem].buildableObject, mousePos, new Quaternion(0, 0, 0, 0));
        }
    }

    void UpdatePhantomSprite()
    {
        Sprite buildSprite = _buildItems[_currentItem].buildableObject.GetComponent<SpriteRenderer>().sprite;
        Vector3 scale = _buildItems[_currentItem].buildableObject.transform.localScale;
        phantomSprite.sprite = buildSprite;
        phantomSprite.transform.localScale = scale;
        phantomSprite.enabled = _buildModeOn;
    }

    public void TestPrint()
    {
        print("test");
    }
}
