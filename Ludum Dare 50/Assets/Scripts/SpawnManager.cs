using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public static SpawnManager instance;

    [SerializeField] private GameObject _infantryPrefab;
    [SerializeField] private GameObject _archerPrefab;
    [SerializeField] private GameObject _cavalryPrefab;
    [SerializeField] private GameObject _enemyInfantryPrefab;
    [SerializeField] private GameObject _enemyArcherPrefab;
    [SerializeField] private GameObject _enemyCavalryPrefab;

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

        switch (unitType)
        {
            case UnitType.INFANTRY:
                unitPrefab = _infantryPrefab;
                break;
            case UnitType.ARCHER:
                unitPrefab = _archerPrefab;
                break;
            case UnitType.CAVALRY:
                unitPrefab = _cavalryPrefab;
                break;
            default:
                Debug.Log("Unkown Prefab");
                unitPrefab = null;
                break;
        }

        currentTile.TileOccupier = Instantiate(unitPrefab, new Vector3(spawnPos.x, spawnPos.y, 0f), transform.rotation, currentTile.transform);
        Debug.Log("Spawned " + unitType + "at " + spawnPos.x + spawnPos.y);
    }

    void SpawnEnemyUnit(Vector2 spawnpos, UnitType unitType, WorldTile currentTile)
    {
        GameObject unitPrefab;

        switch (unitType)
        {
            case UnitType.E_INFANTRY:
                unitPrefab = _infantryPrefab;
                break;
            case UnitType.E_ARCHER:
                unitPrefab = _archerPrefab;
                break;
            case UnitType.E_CAVALRY:
                unitPrefab = _cavalryPrefab;
                break;
        }
    }
}
