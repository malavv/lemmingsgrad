using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chute
{
    public enum Level { lvl1 }

    [Serializable()]
    public class Config
    {
        public int NumToSpawn;
        public float TimeBetweenSpawnInSec;
    }

    public World World { get; protected set; }

    private Player owner;
    public Level level;
    public Config config;
    private int nSpawned = 0;

    int X;
    int Y;
    private float timeSinceLastSpawnInSec = 99f;

    public Chute() {}

    public Chute(int x, int y, Level level, World world, Player owner) {
        X = x;
        Y = y;
        this.level = level;
        World = world;
        this.owner = owner;
    }

    public void FixedUpdate()
    {
        if (config.NumToSpawn == 0)
            return;

        if (timeSinceLastSpawnInSec > config.TimeBetweenSpawnInSec)
        {
            timeSinceLastSpawnInSec = 0;
            World.CreateChar("player." + nSpawned, new Vector2(X + 0.5f, Y + 0.5f), owner);
            config.NumToSpawn--;
            nSpawned++;
        }
        timeSinceLastSpawnInSec += Time.deltaTime;
    }
}
