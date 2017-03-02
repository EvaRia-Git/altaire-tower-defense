using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(IsometricObject))]
[RequireComponent(typeof(SpriteRenderer))]
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
    private SpriteRenderer spriteRenderer;
    
    void Start() {
        tileType = TileType.DEV;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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
