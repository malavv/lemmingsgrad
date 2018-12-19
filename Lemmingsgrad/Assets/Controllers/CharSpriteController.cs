using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharSpriteController : MonoBehaviour
{
    Dictionary<Character, GameObject> GOMap;
    Dictionary<string, Sprite> charSprites;

    World world
    {
        get { return WorldController.Instance.world; }
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadSprites();

        GOMap = new Dictionary<Character, GameObject>();

        Debug.Log(world);

        world.RegisterCharCreated(OnCharCreated);

        world.CreateChar(world.GetTileAt(5, 5));
    }

    private void LoadSprites()
    {
        charSprites = new Dictionary<string, Sprite>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Images/Char/");

        Debug.Log("Loaded Char Sprites");
        foreach (Sprite s in sprites)
        {
            charSprites[s.name] = s;
        }
    }

    private void OnCharCreated(Character c)
    {
        GameObject go = new GameObject();

        GOMap.Add(c, go);

        go.name = "Char_" + c.X + "_" + c.Y;
        go.transform.position = new Vector3(c.X, c.Y, 0);
        go.transform.SetParent(this.transform, true);

        go.AddComponent<SpriteRenderer>();
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        sr.sprite = getSpriteForChar(c);
        sr.sortingLayerName = "Characters";

        c.RegisterOnChangedCallback(OnCharChanged);
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
