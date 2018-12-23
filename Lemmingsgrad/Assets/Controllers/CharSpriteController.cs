using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharSpriteController : MonoBehaviour
{
    Dictionary<Character, GameObject> GOMap;
    Dictionary<string, Sprite> charSprites;

    World world { get { return WorldController.Instance.world; } }

    // Start is called before the first frame update
    void Start()
    {
        GOMap = new Dictionary<Character, GameObject>();
        charSprites = new Dictionary<string, Sprite>();

        LoadSprites();

        world.RegisterCharCreated(OnCharCreated);
    }

    private void LoadSprites()
    {
        foreach (Sprite s in Resources.LoadAll<Sprite>("Images/Char/"))
            charSprites[s.name] = s;
        Debug.Log("Loaded Char Sprites");
    }

    private void OnCharCreated(Character c)
    {
        GameObject go = new GameObject();
        GOMap.Add(c, go);

        go.name = "Char_" + c.Name;
        go.transform.position = new Vector3(c.Origin.x, c.Origin.y, 0);
        go.transform.SetParent(this.transform, true);

        go.AddComponent<SpriteRenderer>();
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        sr.sprite = getSpriteForChar(c);
        sr.sortingLayerName = "Characters";

        go.AddComponent<Rigidbody2D>();
        go.AddComponent<BoxCollider2D>();

        BoxCollider2D bc = go.GetComponent<BoxCollider2D>();
        bc.offset = new Vector2(0, 0);
        bc.size = new Vector2(1, 1);

        c.RegisterOnChangedCallback(OnCharChanged);

        OnCharChanged(c);
    }

    private void OnCharChanged(Character c)
    {
        if (!GOMap.ContainsKey(c))
        {
            Debug.LogError("OnCharChanged");
            return;
        }
        GameObject go = GOMap[c];
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        sr.sprite = getSpriteForChar(c);
        sr.sortingLayerName = "Characters";
    }

    private Sprite getSpriteForChar(Character c)
    {
        return charSprites["front_hold"];
    }
}
