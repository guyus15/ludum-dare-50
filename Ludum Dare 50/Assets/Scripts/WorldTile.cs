using System;
using System.Collections;
using UnityEngine;

public enum TileAreaType
{
    FARMLAND = 0,
    FOREST,
    MOUNTAIN,
    FORTRESS,
    CITY,
    MINE
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
    [SerializeField] private Sprite _unknownTileSprite;
    
    private float _xCoords;
    private float _yCoords;

    private bool _underAttack;
    private bool _enemyOwned;
    private bool _highlighted;
    
    private int _numTurnsUnderAttack;
    private int _income;
    private int _population;
    
    private TileAreaType _tileAreaType;
    private TileResourceType _tileResourceType;

    private SpriteRenderer _spriteRenderer;
    public Sprite TileSprite { get; private set; }

    private void Start()
    {
        _underAttack = false;
        _enemyOwned = false;
        _highlighted = false;

        // Picking a random area and resource type for the tile.
        
        Array tileAreaTypes = Enum.GetValues(typeof(TileAreaType));
        _tileAreaType = (TileAreaType)tileAreaTypes.GetValue(UnityEngine.Random.Range(0, tileAreaTypes.Length));
        
        _tileResourceType = TileResourceType.FOOD;

        _spriteRenderer = GetComponent<SpriteRenderer>();

        AssignTileSprite();
    }

    public void SetCoordinates(float xCoords, float yCoords)
    {
        _xCoords = xCoords;
        _yCoords = yCoords;
    }
    
    private void AssignTileSprite()
    {
        switch (_tileAreaType)
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

    public static int GetTileSize()
    {
        return TILE_SIZE;
    }

    public bool IsUnderAttack()
    {
        return _underAttack;
    }

    public bool IsEnemyOwned()
    {
        return _enemyOwned;
    }
}
