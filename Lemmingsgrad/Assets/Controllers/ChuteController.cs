using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuteController : MonoBehaviour
{
    World World { get { return WorldController.Instance.world; } }

    public Chute.Config Level1;

    void Start() {
        World.RegisterChuteCreated(OnCreated);
    }

    private void OnCreated(Chute obj)
    {
        obj.config = getConfig(obj);
    }

    private Chute.Config getConfig(Chute chute)
    {
        switch (chute.level)
        {
            case Chute.Level.lvl1:
                return Level1;
            default:
                return new Chute.Config();
        }
    }

    void Update() { }
}
