using System;
using UnityEngine;
using UnityEngine.UI;

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

    public TileAreaType TileArea { get; private set; }
    
    public Sprite TileSprite { get; private set; }
    
    public GameObject TileOccupier { get; set; }

    [SerializeField] private GameObject _colourTile;
    
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _colourTileSpriteRenderer;
    
    private void Awake()
    {
        _colourTile.SetActive(false);
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _colourTileSpriteRenderer = _colourTile.GetComponent<SpriteRenderer>();
        
        UnderAttack = false;
        EnemyOwned = false;

        // Picking a random area and resource type for the tile.
        
        Array tileAreaTypes = Enum.GetValues(typeof(TileAreaType));
        TileArea = (TileAreaType)tileAreaTypes.GetValue(UnityEngine.Random.Range(0, tileAreaTypes.Length - 1));

        AssignTileSprite();
    }

    private void Update()
    {
        if (TileOccupier != null)
        {
            UnitBehaviour unit = TileOccupier.GetComponent<UnitBehaviour>();

            if (unit.Allied)
            {
                _colourTile.SetActive(true);
                _colourTileSpriteRenderer.color =  new Color(0, 0, 255, 128);
            }
            else
            {
                _colourTile.SetActive(true);
                _colourTileSpriteRenderer.color =  new Color(255, 0, 0, 128);
            }
            
            return;
        }

        if (EnemyOwned)
        {
            _colourTile.SetActive(true);
            _colourTileSpriteRenderer.color = new Color(0, 0, 0, 128);
        }
        else
        {
            _colourTile.SetActive(false);
        }
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
                Income = Constants.FARMLAND_TURN_INCOME;
                break;
            case TileAreaType.FOREST:
                TileSprite = _forestTileSprite;
                Income = Constants.FOREST_TURN_INCOME;
                break;
            case TileAreaType.MOUNTAIN:
                TileSprite = _mountainTileSprite;
                Income = Constants.MOUNTAIN_TURN_INCOME;
                break;
            case TileAreaType.FORTRESS:
                TileSprite = _fortressTileSprite;
                Income = Constants.FORTRESS_TURN_INCOME;
                break;
            case TileAreaType.MINE:
                TileSprite = _mineTileSprite;
                Income = Constants.MINE_TURN_INCOME;
                break;
            case TileAreaType.CITY:
                TileSprite = _cityTileSprite;
                Income = Constants.CITY_TURN_INCOME;
                break;
            case TileAreaType.PLAINS:
                TileSprite = _plainsTileSprite;
                Income = Constants.PLAINS_TURN_INCOME;
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
