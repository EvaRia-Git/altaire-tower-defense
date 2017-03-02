using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IsoTile : MonoBehaviour
{

    public enum TileType
    {
        DEV     = 000,
        BLACK   = 001,
        WHITE   = 002
    }

    public static Sprite LoadSprite(TileType id)
    {
        return Resources.Load<Sprite>(string.Format("Textures/Tiles/tile-{0:D3}", (int)id));
    }

    public TileType tileType;
    IsometricObject isoProperties;
    SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start() {
        tileType = TileType.DEV;
        isoProperties = gameObject.AddComponent<IsometricObject>();
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.hideFlags = HideFlags.HideInInspector;
        UpdateTexture();
    }

    void OnValidate() {
        UpdateTexture();
    }

    public void UpdateTexture() {
        spriteRenderer.sprite = LoadSprite(tileType);
    }
}
