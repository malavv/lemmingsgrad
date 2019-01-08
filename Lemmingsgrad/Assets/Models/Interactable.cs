using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable {
    public enum Type { Door };

    public Type type { get; protected set; }
    public World World { get; protected set; }

    public int X, Y;

    private Action<Interactable> onChanged;

    public Interactable(Type type, World world, int x, int y)
    {
        this.type = type;
        this.World = world;
        this.X = x;
        this.Y = y;
    }

    public void RegisterOnChangedCallback(Action<Interactable> callback) { onChanged += callback; }
    public void UnRegisterOnChangedCallback(Action<Interactable> callback) { onChanged -= callback; }
}
