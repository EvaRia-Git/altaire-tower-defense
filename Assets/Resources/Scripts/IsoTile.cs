using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(IsometricObject))]
[RequireComponent(typeof(SpriteRenderer))]
public class IsoTile : MonoBehaviour
{
    // Holds Numeric IDs for each type of tile we create.
    // New Tiles should be added to this list as we make them.
    public enum TileType
    {
        DEV     = 000,
        BLACK   = 001,
        WHITE   = 002
    }

    // Loads a Sprite from our Textures folder based on the TileType.
    // Tile images must be named like "tile-###" and listed in the enum or this will fail.
    // TODO: We will want to change to a SpriteSheet lookup using source rectangles later for efficiency.
    public static Sprite LoadSprite(TileType id)
    {
        return Resources.Load<Sprite>(string.Format("Textures/Tiles/tile-{0:D3}", (int)id));
    }

    // Declare Fields
    public TileType tileType;             // The type of tile out of available tiles.
    public float height;                    // Pass ability to control height up one level.
    private SpriteRenderer spriteRenderer;  // The SpriteRenderer used to change the tile's sprite.
    public IsometricObject isoProperties;
    
    // Initialize
    void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        isoProperties = gameObject.GetComponent<IsometricObject>();
        // We hide the SpriteRenderer in the Inspector since we want to control the Sprite using TileType.
        spriteRenderer.hideFlags = HideFlags.HideInInspector;
        UpdateTexture();
    }

    void OnValidate() {
        UpdateTexture();
        UpdateHeight();
    }

    // Updates the Tile's Sprite based on the TileType.
    public void UpdateTexture() {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = LoadSprite(tileType);
        }
    }

    // Updates Height
    public void UpdateHeight()
    {
        isoProperties.isoPosition.isoHeight = height;
        isoProperties.UpdateRealPosition();
    }
}
