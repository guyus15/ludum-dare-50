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
    private List<GameObject> _spawnTileObjects;
    
    private Camera _mainCamera;
    
    private RaycastHit2D _lastTileHit;

    private WorldTile selectedTile = null;
    public bool buildState = false;
    public bool moveState = false;
    public bool attackState = false;
    public GameObject selectedUnit = null;
    public UnitType desiredUnit;

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
    }
    #endregion
   
    private void Start()
    {
        // Create the world tiles for the game.
        _tileCoords = new List<Vector2>();
        _spawnTileObjects = new List<GameObject>();

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
    }

    private void Update()
    {
        RaycastHit2D hit = HoveredTile();

        if(Input.GetKeyDown(KeyCode.B))
        {
            buildState = true;
            moveState = false;
            attackState = false;
            Debug.Log("Buildstate = " + buildState);
            Debug.Log("Movestate = " + moveState);
            Debug.Log("Attackstate = " + attackState);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            buildState = true;
            moveState = false;
            attackState = false;
            Debug.Log("Buildstate = " + buildState);
            Debug.Log("Movestate = " + moveState);
            Debug.Log("Attackstate = " + attackState);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (selectedUnit != null)
            {
                attackState = true;
                buildState = false;
                moveState = false;
                Debug.Log("Attackstate = " + attackState);
                Debug.Log("Buildstate = " + buildState);
                Debug.Log("Movestate = " + moveState);
            }
            else
            {
                Debug.Log("Cannont enter movestate without a unit selected");
            }
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (selectedUnit != null)
            {
                attackState = false;
                buildState = false;
                moveState = true;
                Debug.Log("Attackstate = " + attackState);
                Debug.Log("Buildstate = " + buildState);
                Debug.Log("Movestate = " + moveState);
            }
            else
            {
                Debug.Log("Cannont enter movestate without a unit selected");
            }
        }
        if (buildState)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                desiredUnit = UnitType.INFANTRY;
                Debug.Log("Chosen unit: " + desiredUnit);
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                desiredUnit = UnitType.ARCHER;
                Debug.Log("Chosen unit: " + desiredUnit);
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                desiredUnit = UnitType.CAVALRY;
                Debug.Log("Chosen unit: " + desiredUnit);
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
                if (buildState)
                {
                    switch (hitTile.TileOccupier)
                    {
                        case null:
                            {
                                SpawnManager.instance.SpawnAllyUnit(new Vector2(hitTile.XCoords, hitTile.YCoords), desiredUnit, hitTile);
                                Debug.Log("Spawning allied unit");
                                buildState = false;
                                Debug.Log("Buildstate = " + buildState);
                                selectedTile = null;
                                break;
                            }
                        default:
                            {
                                Debug.Log("Unable to spawn");
                                buildState = false;
                                break;
                            }
                    }
                    
                }
                //Moves selected unit to clicked tile if tile is empty
                else if (moveState)
                {
                    switch (hitTile.TileOccupier)
                    {
                        case null:
                            {
                                selectedUnit.GetComponent<UnitBehaviour>().MoveToTile(selectedUnit, selectedTile, hitTile);
                                Debug.Log($"Moved {selectedUnit} to {hitTile.XCoords}, {hitTile.YCoords}");
                                moveState = false;
                                Debug.Log("Buildstate = " + buildState);
                                selectedTile = null;
                                selectedUnit = null;
                                break;
                            }
                        default:
                            {                                
                                Debug.Log("Tile Occupied");
                                moveState = false;
                                break;
                            }
                    }

                }
                //Attacks a unit if clicked tile holds an enemy
                else if (attackState)
                {
                    if (selectedTile == hitTile)
                    {
                        Debug.Log("Invalid Tile - Cannot attack self");
                        attackState = false;
                    }
                    else
                    {                    
                        switch (hitTile.TileOccupier)
                        {
                            case null:
                                {
                                    Debug.Log("Invalid attack - Empty Tile");
                                    attackState = false;
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
                                        selectedUnit.GetComponent<UnitBehaviour>().AttackUnit(selectedUnit, hitTile.TileOccupier);
                                        
                                    }
                                    attackState = false;
                                    break;
                                }
                        }
                    }
                }                
                //Selects a tile and unit in the tile where applicable
                else if (!(moveState || buildState || attackState))
                {
                    selectedTile = hitTile;
                    Debug.Log("Tile selected");
                    if (hitTile.TileOccupier != null)
                    {
                        selectedUnit = hitTile.TileOccupier;
                        Debug.Log("Unit selected");                                                
                    }
                    else
                    {
                        selectedUnit = null;
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
            
            worldTile.TileOccupierEnemy = enemy;
            worldTile.TileOccupier = enemy;
        }
    }
    
    public int GetGridSize()
    {
        return _gridSize;
    }
    
    public RaycastHit2D HoveredTile()
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
