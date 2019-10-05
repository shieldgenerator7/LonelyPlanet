using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public float spawnDelay = 1;
    public float maxRadiusFromCenter = 100;

    public List<GameObject> spawnPrefabs;

    private float lastSpawnTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastSpawnTime + spawnDelay)
        {
            lastSpawnTime = Time.time;

            //Spawn Object
            int randIndex = Random.Range(0, spawnPrefabs.Count);
            GameObject newThing = Instantiate(spawnPrefabs[randIndex]);
            Vector2 pos = Vector2.zero;
            pos.x = Random.Range(-maxRadiusFromCenter, maxRadiusFromCenter);
            pos.y = Random.Range(-maxRadiusFromCenter, maxRadiusFromCenter);
            if (Vector2.Distance(pos, Vector2.zero) > maxRadiusFromCenter)
            {
                pos = pos.normalized * maxRadiusFromCenter;
            }
            newThing.transform.position = pos;
            newThing.transform.parent = transform;
        }
    }
}
