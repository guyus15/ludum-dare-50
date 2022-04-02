using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    public static WorldGrid _instance;

    private Vector2 _startingCoords;
    
    [SerializeField] private int _gridSize;
    [SerializeField] private GameObject _worldTilePrefab;

    private List<Vector2> _tileCoords;

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

        BuildWorld();
    }

    private void BuildWorld()
    {
        Debug.Log("Building the world");
        
        foreach (Vector2 coords in _tileCoords)
        {
            Vector3 convertedCoords = new Vector3(coords.x, coords.y, 0f);
            GameObject newTile = Instantiate(_worldTilePrefab, convertedCoords, transform.rotation);
            
            // Set random tile attributes here.
        }
    }
}
