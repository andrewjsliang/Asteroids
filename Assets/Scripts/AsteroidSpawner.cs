﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Asteroid asteroidPrefab;
    public float trajectoryVariance = 15.0f;
    public float spawnRate = 2.0f;
    public float spawnDistance = 10.0f;
    public int spawnAmount = 1;
    void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn() 
    {
        for (int i = 0; i < this.spawnAmount; i++) 
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPoint = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
