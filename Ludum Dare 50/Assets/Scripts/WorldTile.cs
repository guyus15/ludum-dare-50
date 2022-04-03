using System;
using UnityEngine;

public enum TileAreaType
{
    FARMLAND = 0,
    FOREST,
    MOUNTAIN,
    FORTRESS,
    CITY,
    MINE,
    PLAINS,
    SPAWN
}

public enum TileResourceType
{
    STONE = 0,
    GOLD,
    FOOD
}

public class WorldTile : MonoBehaviour
{
    // The X and Y dimensions for a tile.
    private static int TILE_SIZE = 200;

    // Defining the sprites for each tile.
    [SerializeField] private Sprite _farmlandTileSprite;
    [SerializeField] private Sprite _forestTileSprite;
    [SerializeField] private Sprite _mountainTileSprite;
    [SerializeField] private Sprite _fortressTileSprite;
    [SerializeField] private Sprite _cityTileSprite;
    [SerializeField] private Sprite _mineTileSprite;
    [SerializeField] private Sprite _plainsTileSprite;
    [SerializeField] private Sprite _spawnTileSprite;
    [SerializeField] private Sprite _unknownTileSprite;

    public float XCoords { get; private set; }
    public float YCoords { get; private set; }

    public bool UnderAttack { get; set; }
    public bool EnemyOwned { get; set; }

    public int TurnsUnderAttack { get; private set; }
    public int Income { get; private set; }
    public int Population { get; private set; }

    public TileAreaType TileArea { get; private set; }
    public TileResourceType TileResource { get; private set; }

    private SpriteRenderer _spriteRenderer;
    public Sprite TileSprite { get; private set; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        UnderAttack = false;
        EnemyOwned = false;

        // Picking a random area and resource type for the tile.
        
        Array tileAreaTypes = Enum.GetValues(typeof(TileAreaType));
        TileArea = (TileAreaType)tileAreaTypes.GetValue(UnityEngine.Random.Range(0, tileAreaTypes.Length - 1));
        
        TileResource = TileResourceType.FOOD;
        
        AssignTileSprite();
    }
    
    public void SetCoordinates(float xCoords, float yCoords)
    {
        XCoords = xCoords;
        YCoords = yCoords;
    }
    
    private void AssignTileSprite()
    {
        switch (TileArea)
        {
            case TileAreaType.FARMLAND:
                TileSprite = _farmlandTileSprite;
                break;
            case TileAreaType.FOREST:
                TileSprite = _forestTileSprite;
                break;
            case TileAreaType.MOUNTAIN:
                TileSprite = _mountainTileSprite;
                break;
            case TileAreaType.FORTRESS:
                TileSprite = _fortressTileSprite;
                break;
            case TileAreaType.MINE:
                TileSprite = _mineTileSprite;
                break;
            case TileAreaType.CITY:
                TileSprite = _cityTileSprite;
                break;
            case TileAreaType.PLAINS:
                TileSprite = _plainsTileSprite;
                break;
            case TileAreaType.SPAWN:
                TileSprite = _spawnTileSprite;
                break;
            default:
                TileSprite = _unknownTileSprite;
                Debug.Log("A tile sprite does not exist for this tile type.");
                break;
        }

        _spriteRenderer.sprite = TileSprite;
    }

    public void HighlightTile(bool highlight)
    {
        _spriteRenderer.color = highlight ? new Color(1f, 1f, 1f, 0.5f)
            : new Color(1f, 1f, 1f, 1f);
    }

    public void MarkAsSpawnTile()
    {
        TileArea = TileAreaType.SPAWN;
        AssignTileSprite();
    }
    
    public static int GetTileSize()
    {
        return TILE_SIZE;
    }
}
