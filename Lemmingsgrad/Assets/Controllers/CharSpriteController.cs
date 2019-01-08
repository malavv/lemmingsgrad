using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharSpriteController : MonoBehaviour
{
    World world { get { return WorldController.Instance.world; } }

    Dictionary<Character, GameObject> GOMap;
    Dictionary<string, Sprite> charSprites;

    // Start is called before the first frame update
    void Start()
    {
        GOMap = new Dictionary<Character, GameObject>();
        charSprites = new Dictionary<string, Sprite>();

        LoadSprites();

        Debug.Log("On world registering");
        Debug.Log(world);
        world.RegisterCharCreated(OnCharCreated);

        foreach (Character character in world.characters)
            OnCharCreated(character);
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
        go.layer = 8;

        go.transform.position = new Vector3(c.Origin.x, c.Origin.y, 0);
        go.transform.SetParent(this.transform, true);

        go.AddComponent<SpriteRenderer>();
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        sr.sprite = getSpriteForChar(c);
        sr.sortingLayerName = "Characters";

        go.AddComponent<Rigidbody2D>();
        go.AddComponent<CircleCollider2D>();
        go.AddComponent<CharController>();

        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        CircleCollider2D bc = go.GetComponent<CircleCollider2D>();
        bc.offset = new Vector2(0f, 0f);
        bc.radius = 0.5f;

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

    void FixedUpdate() { }
}
