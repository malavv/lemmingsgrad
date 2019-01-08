using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public static WorldController Instance { get; protected set; }
    public World world { get; protected set; }

    public void OnEnable()
    {
        Instance = this;
        world = new World(50, 25);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Creating Tiles");
        drawFloor();
        drawWalls();
        

        Debug.Log("Creating Chute");
        Interactable door = world.CreateInteractable(5, 20, Interactable.Type.Door);
        world.CreateChute(door.X, door.Y, Chute.Level1);
    }

    void drawFloor()
    {
        for (int x = 0; x < world.Width; x++)
            world.CreateTile(x, 0, Tile.Type.Obsidian);
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
        foreach (Chute chute in world.chutes)
            chute.FixedUpdate();
    }
}
