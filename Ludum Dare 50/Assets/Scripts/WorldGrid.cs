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

    public bool buildState = false;
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
        if(Input.GetKeyDown(KeyCode.B))
        {
            buildState = true;
            Debug.Log("Buildstate now true");
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

        RaycastHit2D hit = Physics2D.Raycast(
            _mainCamera.ScreenToWorldPoint(Input.mousePosition), 
            Vector2.zero,
            Mathf.Infinity,
            _tileLayerMask
        );

        if (hit.collider != null)
        {
            WorldTile hitTile = hit.collider.gameObject.GetComponent<WorldTile>();
            hitTile.HighlightTile(true);        
        
            if (!hit.collider.Equals(_lastTileHit.collider) && _lastTileHit.collider != null)
            {
                WorldTile lastHitTile = _lastTileHit.collider.gameObject.GetComponent<WorldTile>();

                lastHitTile.HighlightTile(false);
            }

            if (Input.GetMouseButtonDown(0))
            {
                MenuManager.instance.ShowTileMenu(hitTile);

                //GameObject selectedUnit = hitTile.TileOccupier;
                //Debug.Log($"Selected {selectedUnit} at {hitTile.XCoords}, {hitTile.YCoords}");

                if (buildState)
                {
                    SpawnManager.instance.SpawnAllyUnit(new Vector2(hitTile.XCoords, hitTile.YCoords), desiredUnit, hitTile);
                    buildState = false;
                }
                /*if (selectedUnit != null)
                {
                    selectedUnit.GetComponent<UnitBehaviour>().MoveToTile(selectedUnit, hitTile);
                    Debug.Log($"Moved {selectedUnit} to {hitTile.XCoords}, {hitTile.YCoords}");
                }
                */
                
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

    public int GetGridSize()
    {
        return _gridSize;
    }
}
