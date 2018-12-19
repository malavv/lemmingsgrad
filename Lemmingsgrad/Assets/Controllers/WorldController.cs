using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public static WorldController Instance { get; protected set; }
    public World world { get; protected set; }

    public Sprite ThingSpriteCache;
    public Sprite EmptySpriteCache;
    public Sprite ObsidianSpriteCache;
    public Sprite DoorRSpriteCache;
    public Sprite DoorLSpriteCache;

    public void OnEnable()
    {
        Instance = this;
        world = new World();
    }

    // Start is called before the first frame update
    void Start()
    {
        Dictionary<Tile.Type, Sprite> SpritesMap = new Dictionary<Tile.Type, Sprite> {
            { Tile.Type.Empty, EmptySpriteCache },
            { Tile.Type.Thing, ThingSpriteCache },
            { Tile.Type.Obsidian, ObsidianSpriteCache },
            { Tile.Type.DoorL, DoorLSpriteCache },
            { Tile.Type.DoorR, DoorRSpriteCache },
        };

        int doorHeight = world.height - 5;

        world.GetTileAt(5, doorHeight).type = Tile.Type.DoorL;
        world.GetTileAt(6, doorHeight).type = Tile.Type.DoorR;

        makeBorderTileType(Tile.Type.Obsidian);

        // Create GameObjet for each tile
        for (int x = 0; x < world.width; x++)
        {
            for (int y = 0; y < world.height; y++)
            {
                Tile tile_data = world.GetTileAt(x, y);

                if (tile_data.type == Tile.Type.Empty)
                    continue;
                GameObject tile_go = new GameObject();
                tile_go.name = "Tile_" + x + "_" + y;
                tile_go.transform.position = new Vector3(x, y, 0);
                tile_go.transform.SetParent(this.transform, true);

                // Don't set the actual sprite yet
                tile_go.AddComponent<SpriteRenderer>();

                SpriteRenderer sr = tile_go.GetComponent<SpriteRenderer>();
                sr.sprite = SpritesMap[tile_data.type];
                sr.sortingLayerName = "Tiles";
            }
        }
    }

    void makeBorderTileType(Tile.Type type)
    {
        for (int i = 0; i < world.width; i++)
        {
            world.GetTileAt(i, 0).type = type;
            world.GetTileAt(i, world.height - 1).type = type;
        }
        for (int i = 0; i < world.height; i++)
        {
            world.GetTileAt(0, i).type = type;
            world.GetTileAt(world.width - 1, i).type = type;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
