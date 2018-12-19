using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public float X {
        get { return (destTile.x - currTile.x) * movementPercentage + currTile.x; }
    }
    public float Y
    {
        get { return (destTile.y - currTile.y) * movementPercentage + currTile.y; }
    }

    Tile currTile;
    Tile destTile;
    float movementPercentage;

    float speed = 2f; /* Tiles per seconds */
    private Action<Character> cbCharChanged;

    public void Update(float deltaTime)
    {
        if (currTile == destTile)
            return;

        float distX = (destTile.x - currTile.x);
        float distY = (destTile.x - currTile.x);
        float dist = Mathf.Sqrt(Mathf.Pow(distX, 2) + Mathf.Pow(distY, 2));

        float distThisFrame = speed * deltaTime;

        float percThisFrame = distThisFrame / dist;

        movementPercentage += percThisFrame;

        if (movementPercentage >= 1)
        {
            // Reached destination
            currTile = destTile;
            movementPercentage = 0;
        }
    }

    public Character(Tile tile)
    {
        currTile = tile;
        destTile = tile;
    }

    public void SetDestination(Tile tile)
    {
        destTile = tile;
    }

    public void RegisterOnChangedCallback(Action<Character> callback)
    {
        cbCharChanged += callback;
    }
}
