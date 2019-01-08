﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public static WorldController Instance { get; protected set; }
    public World world { get; protected set; }
    public Player player1;

    public void OnEnable()
    {
        Instance = this;
        world = new World(50, 35);
        player1 = new Player(1, "Player 1");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Creating Tiles");
        drawFloor();
        drawGround(15);
        drawWalls();
        
        Debug.Log("Creating Chute");
        Interactable door = world.CreateInteractable(5, 30, Interactable.Type.Door);
        world.CreateChute(door.X, door.Y, Chute.Level.lvl1, player1);
    }

    void drawFloor()
    {
        for (int x = 0; x < world.Width; x++)
            world.CreateTile(x, 0, Tile.Type.Obsidian);
    }

    void drawGround(int maxHeight)
    {
        for (int x = 1; x < world.Width - 1; x++)
        {
            for (int y = 1; y < maxHeight; y++)
            {
                world.CreateTile(x, y, Tile.Type.Ground);
            }
        }
            
    }

    void drawWalls()
    {
        int leftWall = 0;
        int rightWall = world.Width - 1;
        for (int y = 0; y < world.Height; y++) {
            world.CreateTile(leftWall, y, Tile.Type.Obsidian);
            world.CreateTile(rightWall, y, Tile.Type.Obsidian);
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        world.chutes.ForEach(c => c.FixedUpdate());
        world.characters.ForEach(c => c.FixedUpdate());
    }
}
