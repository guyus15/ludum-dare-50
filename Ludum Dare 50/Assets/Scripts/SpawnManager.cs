using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _infantryPrefab;
    [SerializeField] private GameObject _archerPrefab;
    [SerializeField] private GameObject _cavalryPrefab;
    [SerializeField] private GameObject _enemyInfantryPrefab;
    [SerializeField] private GameObject _enemyArcherPrefab;
    [SerializeField] private GameObject _enemyCavalryPrefab;


    void SpawnAllyUnit(Vector2 mousePos, UnitType unitType) //Create game object of unit type
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

        Instantiate(unitPrefab, new Vector3(mousePos.x, mousePos.y, 0f), transform.rotation);
    }

    void SpawnEnemyUnit(UnitType unitType)
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
