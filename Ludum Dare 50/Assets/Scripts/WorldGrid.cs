using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    public static WorldGrid _instance;

    private Vector2 _startingCoords;
    
    [SerializeField] private int _gridSize;
    [SerializeField] private GameObject _worldTilePrefab;
    [SerializeField] private GameObject _tileContainer;
    [SerializeField] private LayerMask _tileLayerMask;
    
    private List<Vector2> _tileCoords;

    private Camera _mainCamera;

    RaycastHit2D _lastTileHit;
    
    #region Singleton
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        } else if (_instance != this)
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
            }
            
            _lastTileHit = hit;
        }
        else
        {
            if (_lastTileHit.collider != null)
            {
                WorldTile hitTile = _lastTileHit.collider.gameObject.GetComponent<WorldTile>();
                hitTile.HighlightTile(false);
            }
        }
    }

    private void BuildWorld()
    {
        foreach (Vector2 coords in _tileCoords)
        {
            Vector3 convertedCoords = new Vector3(coords.x - (_gridSize / 2), coords.y - (_gridSize / 2), 0f);

            GameObject newTile = Instantiate(
                _worldTilePrefab,
                convertedCoords,
                transform.rotation,
                _tileContainer.transform
            );

            newTile.GetComponent<WorldTile>().SetCoordinates(coords.x, coords.y);
        }
    }
}
