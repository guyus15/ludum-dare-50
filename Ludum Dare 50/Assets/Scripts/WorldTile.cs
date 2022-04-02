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
    [SerializeField] private Sprite farmlandTileSprite;
    [SerializeField] private Sprite forestTileSprite;
    [SerializeField] private Sprite mountainTileSprite;
    [SerializeField] private Sprite fortressTileSprite;
    [SerializeField] private Sprite cityTileSprite;
    [SerializeField] private Sprite mineTileSprite;
    [SerializeField] private Sprite unknownTileSprite;
    
    private int _xCoords;
    private int _yCoords;

    private bool _underAttack;
    private bool _enemyOwned;

    private int _numTurnsUnderAttack;
    private int _income;
    private int _population;
    
    private TileAreaType _tileAreaType;
    private TileResourceType _tileResourceType;

    private Sprite _tileSprite;
    
    public WorldTile(int xCoords, int yCoords)
    {
        _xCoords = xCoords;
        _yCoords = yCoords;

        _underAttack = false;
        _enemyOwned = false;

        _tileAreaType = TileAreaType.FARMLAND;
        _tileResourceType = TileResourceType.FOOD;

        AssignTileSprite();
    }

    private void AssignTileSprite()
    {
        switch (_tileAreaType)
        {
            case TileAreaType.FARMLAND:
                _tileSprite = farmlandTileSprite;
                break;
            case TileAreaType.FOREST:
                _tileSprite = farmlandTileSprite;
                break;
            case TileAreaType.MOUNTAIN:
                _tileSprite = farmlandTileSprite;
                break;
            case TileAreaType.FORTRESS:
                _tileSprite = farmlandTileSprite;
                break;
            case TileAreaType.MINE:
                _tileSprite = farmlandTileSprite;
                break;
            case TileAreaType.CITY:
                _tileSprite = farmlandTileSprite;
                break;
            default:
                _tileSprite = unknownTileSprite;
                Debug.Log("A tile sprite does not exist for this tile type.");
                break;
        }        
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
