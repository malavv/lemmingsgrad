using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;

public class Tile : IXmlSerializable
{
    public enum Type { Empty, Thing, Obsidian, Ground };

    public Type type { get; protected set; }

    public int X, Y;

    private Action<Tile> onChanged;

    public Tile() { }

    public Tile(Type type, World world, int x, int y)
    {
        this.type = type;
        X = x;
        Y = y;
    }

    public void SetType(Type type)
    {
        this.type = type;
        if (onChanged != null)
            onChanged(this);
    }

    public void RegisterOnChangedCallback(Action<Tile> callback) { onChanged += callback; }
    public void UnRegisterOnChangedCallback(Action<Tile> callback) { onChanged -= callback; }

    public XmlSchema GetSchema() { return null; }

    public void ReadXml(XmlReader reader) {
        X = int.Parse(reader.GetAttribute("X"));
        Y = int.Parse(reader.GetAttribute("Y"));
        type = (Type)Enum.Parse(typeof(Type), reader.GetAttribute("Type"));
    }

    public void WriteXml(XmlWriter writer) {
        writer.WriteAttributeString("X", X.ToString());
        writer.WriteAttributeString("Y", Y.ToString());
        writer.WriteAttributeString("Type", type.ToString());
    }
}
