using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chute
{
    public static Chute Level1 = new Chute();

    public World World { get; protected set; }

    private int nToSpawn = 10;
    private int nSpawned = 0;
    private float timeBetweenSpawnInSec = 1f;

    int X;
    int Y;
    private float timeSinceLastSpawnInSec = 99f;

    public Chute() {}

    public Chute(int x, int y, Chute chute, World world) {
        X = x;
        Y = y;
        nToSpawn = chute.nToSpawn;
        timeBetweenSpawnInSec = chute.timeBetweenSpawnInSec;
        World = world;
    }

    public void FixedUpdate()
    {
        if (nToSpawn == 0)
            return;

        if (timeSinceLastSpawnInSec > timeBetweenSpawnInSec)
        {
            timeSinceLastSpawnInSec = 0;
            World.CreateChar("player." + nSpawned, new Vector2(X, Y));
            nToSpawn--;
            nSpawned++;
        }
        timeSinceLastSpawnInSec += Time.deltaTime;
    }
}
