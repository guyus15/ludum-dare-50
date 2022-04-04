using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    public static WorldGrid instance;

    private Vector2 _startingCoords;
    
    [SerializeField] private int _gridSize = 10;
    [SerializeField] private GameObject _worldTilePrefab;
    [SerializeField] private GameObject _tileContainer;
    [SerializeField] private LayerMask _tileLayerMask;
    
    private List<Vector2> _tileCoords;

    private Camera _mainCamera;
    
    private RaycastHit2D _lastTileHit;

    private WorldTile selectedTile = null;
    public bool buildState = false;
    public bool moveState = false;
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
        _startingCoords = Vector2.zero;
        _tileCoords = new List<Vector2>();

        int currentRow = 0;
        
        for (int i = 0; i < _gridSize * _gridSize; i++)
        {
            if (i % _gridSize == 0)
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
            Debug.Log("Buildstate = " + buildState);
            Debug.Log("Movestate = " + moveState);
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
                                buildState = false;
                                Debug.Log("Buildstate = " + buildState);
                                selectedTile = null;
                                break;
                            }
                        default:
                            {
                                Debug.Log("Unable to spawn");
                                break;
                            }
                    }
                    
                }

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
                                Debug.Log("Unable to move");
                                break;
                            }
                    }

                }

                else if (!(moveState || buildState))
                {
                    selectedTile = hitTile;
                    Debug.Log("Tile selected");
                    if (hitTile.TileOccupier != null)
                    {
                        selectedUnit = hitTile.TileOccupier;
                        Debug.Log("Unit selected");
                        moveState = true;
                        Debug.Log("Movestate = " + moveState);
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
        foreach (Vector2 coords in _tileCoords)
        {
            Vector3 convertedCoords = new Vector3(coords.x - (_gridSize / 2) + 0.5f, coords.y - (_gridSize / 2), 0f);

            GameObject newTile = Instantiate(
                _worldTilePrefab,
                convertedCoords,
                transform.rotation,
                _tileContainer.transform
            );

            newTile.GetComponent<WorldTile>().SetCoordinates(convertedCoords.x, convertedCoords.y);
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
