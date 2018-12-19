using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public enum Type { Empty, Thing, Obsidian, DoorL, DoorR };

    public Type type = Type.Empty;

    World world;

    public int x, y;

    public Tile(Type type, World world, int x, int y)
    {
        this.type = type;
        this.world = world;
        this.x = x;
        this.y = y;
    }
}
