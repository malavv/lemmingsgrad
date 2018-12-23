using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Left, Right, Unknown };

public class Character
{
    private Action<Character> cbCharChanged;
    public readonly String Name;
    public readonly Vector2 Origin;

    //World world;

    public Direction Direction { get; protected set; }

    public Character(World world, String name, Vector2 origin, Direction direction = Direction.Unknown) {
        //this.world = world;
        Name = name;
        Direction = direction;
        Origin = origin;
    }

    public void RegisterOnChangedCallback(Action<Character> callback) { cbCharChanged += callback; }
    public void UnRegisterOnChangedCallback(Action<Character> callback) { cbCharChanged -= callback; }
}
