using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    public static WorldGrid _instance;
    
    private const int GRID_SIZE = 10;

    private List<WorldTile> worldTiles;

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
        int currentRow = 0;
        
        for (int i = 1; i <= GRID_SIZE * GRID_SIZE; i++)
        {
            if (i % GRID_SIZE == 0)
            {
                currentRow++;
            }

            WorldTile newWorldTile = new WorldTile(i, currentRow);
            worldTiles.Add(newWorldTile);
        }
    }
}
