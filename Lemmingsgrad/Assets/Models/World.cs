using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {

    public Tile[,] tiles;
    public int Width { get; protected set; }
    public int Height { get; protected set; }

    List<Character> characters = new List<Character>();
    private Action<Character> cbCharCreated;
    private Action<Tile> cbTileCreated;

    public World(int width = 50, int height = 25)
    {
        Width = width;
        Height = height;
        tiles = new Tile[width, height];
    }

    public Tile CreateTile(int x, int y, Tile.Type type = Tile.Type.Empty)
    {
        if (x < 0 || x >= Width) Debug.Log("Invalid X for tiles " + x);
        if (y < 0 || y >= Height) Debug.Log("Invalid X for tiles " + y);
        Tile t = new Tile(type, this, x, y);
        tiles[x, y] = t;
        if (cbTileCreated != null)
            cbTileCreated(t);
        return t;
    }

    public Character CreateChar(String name, Vector2 position) {
        Character c = new Character(this, name, position);
        characters.Add(c);
        if (cbCharCreated != null)
            cbCharCreated(c);
        return c;
    }

    public Tile GetTileAt(int x, int y) { return tiles[x, y]; }

    public void RegisterCharCreated(Action<Character> callback) { cbCharCreated += callback; }
    public void RegisterTileCreated(Action<Tile> callback) { cbTileCreated += callback; }
}
