using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    public static WorldGrid instance;

    [SerializeField] private int _gridSize = 10;
    [SerializeField] private GameObject _worldTilePrefab;
    [SerializeField] private GameObject _tileContainer;
    [SerializeField] private LayerMask _tileLayerMask;
    
    private List<Vector2> _tileCoords;
    private List<GameObject> _tileObjects;
    private List<GameObject> _spawnTileObjects;
    
    private Camera _mainCamera;
    
    private RaycastHit2D _lastTileHit;

    private WorldTile _selectedTile = null;
    
    private bool _buildState = false;
    private bool _moveState = false;
    private bool _attackState = false;
    
    private GameObject _selectedUnit = null;
    
    private UnitType _desiredUnit;

    public int GridIncome { get; set; }
    public int GridControlledAreas { get; set; }
    public int GridEnemyOwnedAreas { get; set; }


    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
        
        // Initialise lists
        _tileCoords = new List<Vector2>();
        _tileObjects = new List<GameObject>();
        _spawnTileObjects = new List<GameObject>();

        // Create the world tiles for the game.
        
        int currentRow = 0;
        
        for (int i = 0; i < _gridSize * _gridSize; i++)
        {
            if (i % _gridSize == 0 && i != 0)
            {
                currentRow++;
            }
            
            _tileCoords.Add(new Vector2(i % _gridSize, currentRow));
        }

        _mainCamera = Camera.main;
        
        BuildWorld();
        
        UpdateWorldValues();
    }
    #endregion

    private void Update()
    {
        RaycastHit2D hit = HoveredTile();

        if(Input.GetKeyDown(KeyCode.B))
        {
            _buildState = true;
            _moveState = false;
            _attackState = false;
            Debug.Log("Buildstate = " + _buildState);
            Debug.Log("Movestate = " + _moveState);
            Debug.Log("Attackstate = " + _attackState);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            _buildState = true;
            _moveState = false;
            _attackState = false;
            Debug.Log("Buildstate = " + _buildState);
            Debug.Log("Movestate = " + _moveState);
            Debug.Log("Attackstate = " + _attackState);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if ( _selectedUnit != null)
            {
                _attackState = true;
                _buildState = false;
                _moveState = false;
                Debug.Log("Attackstate = " + _attackState);
                Debug.Log("Buildstate = " + _buildState);
                Debug.Log("Movestate = " + _moveState);
            }
            else
            {
                Debug.Log("Cannont enter movestate without a unit selected");
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (_selectedUnit != null)
            {
                _attackState = false;
                _buildState = false;
                _moveState = true;
                Debug.Log("Attackstate = " + _attackState);
                Debug.Log("Buildstate = " + _buildState);
                Debug.Log("Movestate = " + _moveState);
            }
            else
            {
                Debug.Log("Cannont enter movestate without a unit selected");
            }
        }
        if (_buildState)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                _desiredUnit = UnitType.INFANTRY;
                Debug.Log("Chosen unit: " + _desiredUnit);
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                _desiredUnit = UnitType.ARCHER;
                Debug.Log("Chosen unit: " + _desiredUnit);
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                _desiredUnit = UnitType.CAVALRY;
                Debug.Log("Chosen unit: " + _desiredUnit);
            }
        }

        // Create a ray to determine what tile we have hit.

        

        if (hit.collider != null)
        {
            //Highlighting hovered tile
            WorldTile hitTile = hit.collider.gameObject.GetComponent<WorldTile>();
            hitTile.HighlightTile(true);
                    
            if (!hit.collider.Equals(_lastTileHit.collider) && _lastTileHit.collider != null)
            {
                WorldTile lastHitTile = _lastTileHit.collider.gameObject.GetComponent<WorldTile>();

                lastHitTile.HighlightTile(false);
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                //Showing attributes of tile clicked
                MenuManager.instance.ShowTileMenu(hitTile);

                //Spawn units to tile clicked on
                if (_buildState)
                {
                    switch (hitTile.TileOccupier)
                    {
                        case null:
                            {
                                SpawnManager.instance.SpawnAllyUnit(new Vector2(hitTile.XCoords, hitTile.YCoords), _desiredUnit, hitTile);
                                Debug.Log("Spawning allied unit");
                                _buildState = false;
                                Debug.Log("Buildstate = " + _buildState);
                                _selectedTile = null;
                                break;
                            }
                        default:
                            {
                                Debug.Log("Unable to spawn");
                                _buildState = false;
                                break;
                            }
                    }
                    
                }
                //Moves selected unit to clicked tile if tile is empty
                else if (_moveState)
                {
                    switch (hitTile.TileOccupier)
                    {
                        case null:
                            {
                                _selectedUnit.GetComponent<UnitBehaviour>().MoveToTile(_selectedUnit, _selectedTile, hitTile);
                                Debug.Log($"Moved {_selectedUnit} to {hitTile.XCoords}, {hitTile.YCoords}");
                                _moveState = false;
                                Debug.Log("Buildstate = " + _buildState);
                                _selectedTile = null;
                                _selectedUnit = null;
                                break;
                            }
                        default:
                            {                                
                                Debug.Log("Tile Occupied");
                                _moveState = false;
                                break;
                            }
                    }

                }
                //Attacks a unit if clicked tile holds an enemy
                else if (_attackState)
                {
                    if (_selectedTile == hitTile)
                    {
                        Debug.Log("Invalid Tile - Cannot attack self");
                        _attackState = false;
                    }
                    else
                    {                    
                        switch (hitTile.TileOccupier)
                        {
                            case null:
                                {
                                    Debug.Log("Invalid attack - Empty Tile");
                                    _attackState = false;
                                    break;
                                }
                            default:
                                {
                                    if (hitTile.TileOccupier.GetComponent<UnitBehaviour>().Allied)
                                    {
                                        Debug.Log("Cannot attack allied unit");
                                    }
                                    else
                                    {
                                        _selectedUnit.GetComponent<UnitBehaviour>().AttackUnit(_selectedUnit, hitTile.TileOccupier);
                                    }
                                    _attackState = false;
                                    break;
                                }
                        }
                    }
                }                
                //Selects a tile and unit in the tile where applicable
                else if (!(_moveState || _buildState || _attackState))
                {
                    _selectedTile = hitTile;
                    Debug.Log("Tile selected");
                    if (hitTile.TileOccupier != null)
                    {
                        _selectedUnit = hitTile.TileOccupier;
                        Debug.Log("Unit selected");                                                
                    }
                    else
                    {
                        _selectedUnit = null;
                        Debug.Log("Deselected Unit");
                    }
                }
            }
            
            _lastTileHit = hit;
        }
        else if (_lastTileHit.collider != null)
        {
            WorldTile hitTile = _lastTileHit.collider.gameObject.GetComponent<WorldTile>();
            hitTile.HighlightTile(false);
        }
    }

    private void BuildWorld()
    {
        // Handle building special tiles on which enemies can spawn.
        
        foreach (Vector2 coords in _tileCoords)
        {
            Vector3 convertedCoords = new Vector3(coords.x - (_gridSize / 2) + 0.5f, coords.y - (_gridSize / 2), 0f);

            GameObject newTile = Instantiate(
                _worldTilePrefab,
                convertedCoords,
                transform.rotation,
                _tileContainer.transform
            );
            
            WorldTile newWorldTile = newTile.GetComponent<WorldTile>();

            newWorldTile.SetCoordinates(convertedCoords.x, convertedCoords.y);
            
            // Determine if the tile is a spawn tile.
            if (coords.x == 0 || coords.y == 0 || coords.x == _gridSize - 1 || coords.y == _gridSize - 1)
            {
                newWorldTile.MarkAsSpawnTile();
                _spawnTileObjects.Add(newTile);
            }
            
            _tileObjects.Add(newTile);
        }
    }

    public void SpawnEnemies()
    {
        foreach (GameObject tile in _spawnTileObjects)
        {
            int shouldSpawn = UnityEngine.Random.Range(0, 2);
            if (shouldSpawn != 0) continue;
            
            WorldTile worldTile = tile.GetComponent<WorldTile>();

            GameObject enemy = Instantiate(
                SpawnManager.instance.GetInfantryPrefab(),
                new Vector2(worldTile.XCoords, worldTile.YCoords),
                tile.transform.rotation,
                tile.transform
            );

            worldTile.TileOccupier = enemy;
        }
    }

    public void UpdateWorldValues()
    {
        int totalIncome = 0;
        int totalEnemyTiles = 0;

        foreach (GameObject tile in _tileObjects)
        {
            WorldTile worldTile = tile.GetComponent<WorldTile>();

            GameObject tileOccupier = worldTile.TileOccupier;
            
            if (tileOccupier != null && tileOccupier.GetComponent<UnitBehaviour>().Allied)
            {
                totalIncome += worldTile.Income;
            }

            if (worldTile.EnemyOwned)
            {
                totalEnemyTiles++;
            }
        }

        GridIncome = totalIncome;
        GridControlledAreas = _tileObjects.Count - totalEnemyTiles;
        GridEnemyOwnedAreas = totalEnemyTiles;
    }
    
    public int GetGridSize()
    {
        return _gridSize;
    }
    
    private RaycastHit2D HoveredTile()
    {
        // Create a ray to determine what tile we have hit.
        RaycastHit2D hit = Physics2D.Raycast(
            _mainCamera.ScreenToWorldPoint(Input.mousePosition),
            Vector2.zero,
            Mathf.Infinity,
            _tileLayerMask
        );
        return hit;
    }
}
