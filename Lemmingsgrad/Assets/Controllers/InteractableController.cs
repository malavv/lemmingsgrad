using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    World World { get { return WorldController.Instance.world; } }
    Dictionary<Interactable, GameObject> GOMap;
    Dictionary<string, Sprite> Sprites;

    // Start is called before the first frame update
    void Start()
    {
        GOMap = new Dictionary<Interactable, GameObject>();
        Sprites = new Dictionary<string, Sprite>();

        LoadSprites();

        foreach (Interactable i in World.interactables)
            OnCreated(i);

        World.RegisterInterCreated(OnCreated);
    }

    void OnCreated(Interactable interactable)
    {
        GameObject go = new GameObject();

        GOMap.Add(interactable, go);
        go.name = "Inter_" + interactable.X + "_" + interactable.Y;
        go.transform.SetParent(this.transform, true);

        go.AddComponent<SpriteRenderer>();
        go.AddComponent<BoxCollider2D>();

        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        sr.sortingLayerName = "Interactables";
        

        BoxCollider2D bc = go.GetComponent<BoxCollider2D>();
        bc.offset = GetOffsetFor(interactable);
        bc.size = GetSizeFor(interactable);
        bc.isTrigger = true;

        interactable.RegisterOnChangedCallback(OnChanged);

        OnChanged(interactable);
    }

    void OnChanged(Interactable interactable) {
        if (!GOMap.ContainsKey(interactable))
        {
            Debug.LogError("On Changed With No Known Object");
            return;
        }
        GameObject go = GOMap[interactable];
        go.transform.position = new Vector3(interactable.X, interactable.Y, 0);
        go.GetComponent<SpriteRenderer>().sprite = GetSpriteFor(interactable);
    }

    private Vector2 GetOffsetFor(Interactable t)
    {
        switch (t.type)
        {
            case Interactable.Type.Door:
                return new Vector2(0.5f, 0.5f);
            default:
                return new Vector2(0.5f, 0.5f);
        }
    }
    private Vector2 GetSizeFor(Interactable t)
    {
        switch (t.type)
        {
            case Interactable.Type.Door:
                return new Vector2(2.0f, 1.0f);
            default:
                return new Vector2(1.0f, 1.0f);
        }
    }
    private Sprite GetSpriteFor(Interactable t)
    {
        switch (t.type)
        {
            case Interactable.Type.Door: return Sprites["door"];
            default: return Sprites["na"];
        }
    }

    private void LoadSprites()
    {
        foreach (Sprite s in Resources.LoadAll<Sprite>("Images/Interactables/"))
            Sprites[s.name] = s;

        Debug.Log("Loaded Interactables Sprites");
    }
}
