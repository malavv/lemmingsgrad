using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public enum Type { Empty, Thing, Obsidian, DoorL, DoorR };

    public Type type { get; protected set; }

    //World world;

    public int X, Y;

    private Action<Tile> onChanged;

    public Tile(Type type, World world, int x, int y)
    {
        this.type = type;
        //this.world = world;
        X = x;
        Y = y;
    }

    public void SetType(Type type)
    {
        this.type = type;
        if (onChanged != null)
            onChanged(this);
    }

    public void RegisterOnChangedCallback(Action<Tile> callback) { onChanged += callback; }
    public void UnRegisterOnChangedCallback(Action<Tile> callback) { onChanged -= callback; }
}
