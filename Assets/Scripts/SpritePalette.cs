using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sprite Palette", menuName = "Sprite Palette", order = 0)]
public class SpritePalette : ScriptableObject
{
    public List<Sprite> possibleSprites;

    public Sprite RandomSprite
    {
        get
        {
            return possibleSprites[
                Random.Range(0, possibleSprites.Count)
                ];
        }
    }
}
