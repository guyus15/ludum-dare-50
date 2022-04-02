using System;
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
    private const int TILE_SIZE = 100;

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

        _tileAreaType = TileAreaType.FARMLAND;
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
                TileSprite = _farmlandTileSprite;
                break;
            case TileAreaType.MOUNTAIN:
                TileSprite = _farmlandTileSprite;
                break;
            case TileAreaType.FORTRESS:
                TileSprite = _farmlandTileSprite;
                break;
            case TileAreaType.MINE:
                TileSprite = _farmlandTileSprite;
                break;
            case TileAreaType.CITY:
                TileSprite = _farmlandTileSprite;
                break;
            default:
                TileSprite = _unknownTileSprite;
                Debug.Log("A tile sprite does not exist for this tile type.");
                break;
        }

        _spriteRenderer.sprite = TileSprite;
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
