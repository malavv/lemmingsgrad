using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class WorldController : MonoBehaviour
{
    public static WorldController Instance { get; protected set; }
    public World world { get; protected set; }
    public Player player1;

    static bool loadWorld = false;
    static string worldToLoad = "";

    // On Program Start
    public void OnEnable()
    {
        if (Instance != null)
            Debug.LogError("The should one be one WorldController.");
        Instance = this;

        if (loadWorld)
        {
            CreateWorldFromSaveFile();
            loadWorld = false;
        }
        else
            CreateEmptyWorld();

        player1 = new Player(1, "Player 1");
    }

    void CreateWorldFromSaveFile()
    {
        Debug.Log("WorldController:CreateWorldFromSaveFile");

        TextReader tr = null;
        if (worldToLoad.Equals(""))
        {
            tr = new StringReader(PlayerPrefs.GetString("SaveGame00"));
        } else
        {
            tr = new StreamReader("Assets/Resources/Maps/" + worldToLoad + ".xml");
        }

        XmlSerializer xs = new XmlSerializer(typeof(World));
        world = (World)xs.Deserialize(tr);
        if (world == null)
        {
            Debug.LogError("Error Creating World");
        }
        tr.Close();

        Camera.main.transform.position = new Vector3(10, world.Height / 2, Camera.main.transform.position.z);
    }

    void CreateEmptyWorld()
    {
        Debug.Log("WorldController:CreateEmptyWorld");
        world = new World(50, 25);

        Debug.Log("Creating Tiles");
        drawFloor();
        drawGround(15);
        drawWalls();

        Camera.main.transform.position = new Vector3(10, world.Height / 2, Camera.main.transform.position.z);
    }

    public void Update()
    {
        this.world.Update(Time.deltaTime);
    }

    public void NewWorld()
    {
        Debug.Log("WorldController:NewWorld");

        ReloadActiveScene();
    }

    public void LoadMap1()
    {
        Debug.Log("WorldController:LoadMap1");
        loadWorld = true;
        worldToLoad = "stage1";
        ReloadActiveScene();
    }

    public void SaveWorld()
    {
        Debug.Log("WorldController:SaveWorld");

        XmlSerializer xs = new XmlSerializer(typeof(World));
        TextWriter tw = new StringWriter();

        xs.Serialize(tw, world);
        tw.Close();

        PlayerPrefs.SetString("SaveGame00", tw.ToString());
        Debug.Log(tw.ToString());
    }

    public void LoadWorld()
    {
        Debug.Log("WorldController:LoadWorld");
        loadWorld = true;

        ReloadActiveScene();
    }

    public void UpdateTiles()
    {
        var tc = FindObjectOfType<TileController>();
        var toRemove = new List<Tile>();
        foreach (Tile t in tc.TileMap.Keys)
        {
            GameObject go = tc.TileMap[t];
            if (go == null || !go.activeSelf)
            {
                world.tiles[t.X, t.Y] = null;
                toRemove.Add(t);
                continue;
            }

            world.tiles[t.X, t.Y] = null;
            t.X = (int)Math.Round(go.transform.position.x);
            t.Y = (int)Math.Round(go.transform.position.y);
            world.tiles[t.X, t.Y] = t;
        }

        foreach (Tile t in toRemove)
            tc.TileMap.Remove(t);
        Debug.Log("WorldController: Updated Tile Models");
    }

    void ReloadActiveScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void EmptyWorld()
    {
        world = new World();
    }

    // Start is called before the first frame update
    void Start()
    {

        
        Debug.Log("Creating Chute");
        Interactable door = world.CreateInteractable(5, 23, Interactable.Type.Door);
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
}
