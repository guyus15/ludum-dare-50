using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    [SerializeField] private GameObject _infantryPrefab;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }
    #endregion

    public void SpawnAllyUnit(Vector2 spawnPos, UnitType unitType, WorldTile currentTile) //Create game object of unit type
    {
        GameObject unitPrefab;

        unitPrefab = _infantryPrefab;

        currentTile.TileOccupier = Instantiate(unitPrefab, new Vector3(spawnPos.x, spawnPos.y, 0f), transform.rotation, currentTile.transform);
        Debug.Log("Spawned " + unitType + "at " + spawnPos.x + spawnPos.y);
    }

    public GameObject GetInfantryPrefab()
    {
        return _infantryPrefab;
    }
}
