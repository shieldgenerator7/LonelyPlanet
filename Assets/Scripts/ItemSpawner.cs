using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public float spawnDelay = 1;
    public int maxSpawnLimit = 0;
    public float maxRadiusFromCenter = 100;
    public bool spawnCircular = true;//rectangle if false

    public List<GameObject> spawnPrefabs;
    public ColorPalette colorPalette;
    public SpritePalette spritePalette;
    public float sizeMultiplier = 1;

    private float lastSpawnTime = 0;
    [SerializeField]
    private int itemsSpawned = 0;

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
            if (spawnCircular)
            {
                if (Vector2.Distance(pos, Vector2.zero) > maxRadiusFromCenter)
                {
                    pos = pos.normalized * maxRadiusFromCenter;
                }
            }
            newThing.transform.position = pos;
            newThing.transform.parent = transform;
            //Color
            SpriteRenderer newSR = newThing.GetComponent<SpriteRenderer>();
            newSR.color = colorPalette.RandomColor;
            //Sprite
            newSR.sprite = spritePalette.RandomSprite;
            //Size
            newThing.transform.localScale *= sizeMultiplier;
            newThing.GetComponent<Rigidbody2D>().mass *= sizeMultiplier;
            //
            itemsSpawned++;
        }
        if (maxSpawnLimit > 0 && itemsSpawned >= maxSpawnLimit)
        {
            Destroy(this);
        }
    }
}
