﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TileController : MonoBehaviour {
    World world { get { return WorldController.Instance.world; } }

    public Dictionary<Tile, GameObject> TileMap;
    Dictionary<string, Sprite> tileSprites;

    void Start()
    {
        TileMap = new Dictionary<Tile, GameObject>();
        tileSprites = new Dictionary<string, Sprite>();

        LoadSprites();

        world.RegisterTileCreated(OnTileCreated);

        for (int x = 0; x < world.Width; x++) {
            for (int y = 0; y < world.Height; y++) {
                Tile t = world.tiles[x, y];
                if (t != null)
                    OnTileCreated(t);
            }
        }
    }
    
    private void OnTileCreated(Tile t)
    {
        GameObject go = new GameObject();

        TileMap.Add(t, go);
        go.name = "Tile_" + t.X + "_" + t.Y;
        go.transform.SetParent(this.transform, true);

        go.AddComponent<SpriteRenderer>();
        go.AddComponent<BoxCollider2D>();

        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        sr.sortingLayerName = "Tiles";

        BoxCollider2D bc = go.GetComponent<BoxCollider2D>();
        bc.offset = new Vector2(0.5f, 0.5f);
        bc.size = new Vector2(1.0f, 1.0f);
        bc.edgeRadius = 0.01f;

        t.RegisterOnChangedCallback(OnTileChanged);

        OnTileChanged(t);
    }

    private void OnTileChanged(Tile t)
    {
        if (!TileMap.ContainsKey(t)) {
            Debug.LogError("OnTileChanged");
            return;
        }
        GameObject go = TileMap[t];
        go.transform.position = new Vector3(t.X, t.Y, 0);
        go.GetComponent<SpriteRenderer>().sprite = getSpriteForTile(t);
    }

    private Sprite getSpriteForTile(Tile t)
    {
        switch (t.type)
        {
            case Tile.Type.Empty: return tileSprites["Empty"];
            case Tile.Type.Obsidian: return tileSprites["Obsidian"];
            case Tile.Type.Thing: return tileSprites["Floor"];
            case Tile.Type.Ground: return tileSprites["ground"];
            default: return tileSprites["Floor"];
        }
    }

    private void LoadSprites()
    {
        foreach (Sprite s in Resources.LoadAll<Sprite>("Images/Tiles/"))
            tileSprites[s.name] = s;

        Debug.Log("Loaded Tile Sprites");
    }
}
