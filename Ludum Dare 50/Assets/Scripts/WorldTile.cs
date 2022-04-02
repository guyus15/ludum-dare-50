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
    private const int TILE_SIZE = 100;
    
    private int _xCoords;
    private int _yCoords;

    private bool _underAttack;
    private bool _enemyOwned;

    private int _numTurnsUnderAttack;
    private int _income;
    private int _population;
    
    private TileAreaType _tileAreaType;
    private TileResourceType _tileResourceType;

    public WorldTile(int xCoords, int yCoords)
    {
        _xCoords = xCoords;
        _yCoords = yCoords;

        _underAttack = false;
        _enemyOwned = false;
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
