using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Left, Right, Unknown };

public class Character
{
    private Action<Character> cbCharChanged;
    public readonly String Name;
    public float Speed;
    public readonly Vector2 Origin;
    public bool isGrounded;

    public Direction Direction { get; set; }

    public Character(World world, String name, Vector2 origin, Direction direction = Direction.Unknown) {
        Name = name;
        Direction = direction;
        Origin = origin;
        Speed = 15f;
        isGrounded = false;
    }

    public void RegisterOnChangedCallback(Action<Character> callback) { cbCharChanged += callback; }
    public void UnRegisterOnChangedCallback(Action<Character> callback) { cbCharChanged -= callback; }
}
