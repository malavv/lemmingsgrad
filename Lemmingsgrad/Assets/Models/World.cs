using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

public class World : IXmlSerializable {

    public Tile[,] tiles;
    public List<Interactable> interactables = new List<Interactable>();
    public List<Chute> chutes = new List<Chute>();
    public int Width { get; protected set; }
    public int Height { get; protected set; }

    public List<Character> characters = new List<Character>();
    private Action<Character> cbCharCreated;
    private Action<Tile> cbTileCreated;
    private Action<Interactable> cbInterCreated;
    private Action<Chute> cbChuteCreated;

    public World() { /* Only for serialization */ }

    public World(int width, int height)
    {
        Setup(width, height);
    }

    private void Setup(int width, int height)
    {
        Width = width;
        Height = height;
        tiles = new Tile[width, height];
    }

    internal Chute CreateChute(int x, int y, Chute.Level level1, Player player)
    {
        Chute chute = new Chute(x, y, level1, this, player);
        chutes.Add(chute);
        Debug.Log("Chute Created");
        if (cbChuteCreated != null)
            cbChuteCreated(chute);
        return chute;
    }

    public Tile CreateTile(int x, int y, Tile.Type type = Tile.Type.Empty)
    {
        if (x < 0 || x >= Width) Debug.Log("Invalid X for tiles " + x);
        if (y < 0 || y >= Height) Debug.Log("Invalid X for tiles " + y);
        Tile t = new Tile(type, this, x, y);
        tiles[x, y] = t;
        if (cbTileCreated != null)
            cbTileCreated(t);
        return t;
    }

    internal Interactable CreateInteractable(int x, int y, Interactable.Type door)
    {
        Interactable obj = new Interactable(Interactable.Type.Door, this, x, y);
        interactables.Add(obj);
        Debug.Log("Interactable Created");
        if (cbInterCreated != null)
            cbInterCreated(obj);
        return obj;
    }

    public Character CreateChar(String name, Vector2 position, Player owner) {
        Character c = new Character(this, name, position, owner);
        characters.Add(c);
        Debug.Log("CreateChar");
        if (cbCharCreated != null)
            cbCharCreated(c);
        return c;
    }

    public Tile GetTileAt(int x, int y) { return tiles[x, y]; }

    public void Update(float deltaTime) {}

    public void RegisterCharCreated(Action<Character> callback) { cbCharCreated += callback; }
    public void RegisterTileCreated(Action<Tile> callback) { cbTileCreated += callback; }
    public void RegisterInterCreated(Action<Interactable> callback) { cbInterCreated += callback; }
    public void RegisterChuteCreated(Action<Chute> callback) { cbChuteCreated += callback; }

    public void ReadXml(XmlReader reader) {
        int width = int.Parse(reader.GetAttribute("Width"));
        int height = int.Parse(reader.GetAttribute("Height"));
        Setup(width, height);

        reader.ReadToDescendant("Tiles");
        reader.ReadToDescendant("Tile");
        while (reader.IsStartElement("Tile"))
        {
            Tile tile = new Tile();
            tile.ReadXml(reader);
            tiles[tile.X, tile.Y] = tile;
            reader.ReadToNextSibling("Tile");
        }
    }

    public void WriteXml(XmlWriter writer) {
        writer.WriteAttributeString("Width", Width.ToString());
        writer.WriteAttributeString("Height", Height.ToString());

        writer.WriteStartElement("Tiles");
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (tiles[x, y] == null)
                    continue;
                writer.WriteStartElement("Tile");
                tiles[x, y].WriteXml(writer);
                writer.WriteEndElement();
            }
        }
        writer.WriteEndElement();
    }

    public XmlSchema GetSchema() { return null; }
}
