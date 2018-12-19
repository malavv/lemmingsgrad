using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World {

    public Tile[,] tiles;
    public int width, height;

    List<Character> characters = new List<Character>();
    private Action<Character> cbCharChanged;

    public World(int width = 50, int height = 25)
    {
        this.width = width;
        this.height = height;
        tiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tiles[x, y] = new Tile(Tile.Type.Empty, this, x, y);
            }
        }
        // tiles[width / 4, height / 4]
    }

    public void CreateChar(Tile tile)
    {
        Character c = new Character(tile);
        if (cbCharChanged != null)
            cbCharChanged(c);
    }

    public Tile GetTileAt(int x, int y)
    {
        return tiles[x, y];
    }

    public void RegisterCharCreated(Action<Character> callback)
    {
        cbCharChanged += callback;
    }
}
