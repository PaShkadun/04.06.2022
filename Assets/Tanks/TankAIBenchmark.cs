using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TankAIBenchmark : MonoBehaviour
{
    IUpdatable[] tanks;
    public int numberOfTanks;
    public GameObject tankPrefab;

    // Start is called before the first frame update
    void Start()
    {
        tanks = new IUpdatable[numberOfTanks];
        
        for (int i = 0; i < numberOfTanks; i++)
        {
            var tank = Instantiate(tankPrefab).transform;
            tank.position = new Vector3(Random.Range(-50,50), 0, Random.Range(-50,50));
            var enemyTank = new EnemyTank(tank.transform);

            tanks[i] = enemyTank;
        }
    }

    private void Update()
    {
        foreach (var tank in tanks)
        {
            tank.Tick();
        }
    }
}
