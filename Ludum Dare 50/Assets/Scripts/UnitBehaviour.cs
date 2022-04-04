using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    INFANTRY    = 0,
    ARCHER      = 1,
    CAVALRY     = 2,
    E_INFANTRY = 3,
    E_ARCHER = 4,
    E_CAVALRY = 5,
}
public class UnitBehaviour : MonoBehaviour
{
    [SerializeField] private float _unitHealth;
    [SerializeField] private int _unitMaxHealth;
    [SerializeField] private int _unitStrength;
    [SerializeField] private int _unitLevel;
    [SerializeField] private int _unitMoveSpeed;
    [SerializeField] private int _unitUpkeep;
    [SerializeField] private int _unitCost;
    [SerializeField] private Vector2 _unitPosition;
    [SerializeField] private UnitType _unitType;
    [SerializeField] private TileAreaType _unitPrefTile;

    public bool Allied { get; set; }

    //Movement
    //Attacking 
    //Disband
    //Level up
    //reinforce

    public void MoveToTile(GameObject selectedUnit, WorldTile selectedTile, WorldTile destinationTile)
    {
        // Set selected units coord to the destination tiles coords
        selectedUnit.transform.position = new Vector3(destinationTile.XCoords, destinationTile.YCoords, 0f);
        selectedUnit.transform.parent = destinationTile.transform;
        selectedTile.TileOccupier = null;
        destinationTile.TileOccupier = selectedUnit;

    }

    public void AttackUnit(GameObject attacker, GameObject defender)
    {
        if (CheckAdjacency(attacker, defender))
        {
            attacker.GetComponent<UnitBehaviour>()._unitHealth -= defender.GetComponent<UnitBehaviour>()._unitStrength;
            defender.GetComponent<UnitBehaviour>()._unitHealth -= attacker.GetComponent<UnitBehaviour>()._unitStrength;
            Debug.Log($"{attacker} attacked {defender}");
        }
        else
        {
            Debug.Log("These units are not adjacent");
        }
    }

    public bool CheckAdjacency(GameObject unit1, GameObject unit2)
    {
        if (Mathf.Pow((unit1.GetComponentInParent<WorldTile>().XCoords - unit2.GetComponentInParent<WorldTile>().XCoords), 2f) > 1  || 
            Mathf.Pow((unit1.GetComponentInParent<WorldTile>().YCoords - unit2.GetComponentInParent<WorldTile>().YCoords), 2f) > 1)
        {
            return false;
        }

        return true;
    }

    public void MoveRandomly(GameObject sourceObject, WorldTile sourceTile)
    {
        // Get adjacent tiles
        List<WorldTile> adjacentTiles = new List<WorldTile>();

        foreach (GameObject worldTileObject in WorldGrid.instance.GetWorldTiles())
        {
            WorldTile destTile = worldTileObject.GetComponent<WorldTile>();

            // If the tile is not adjacent, skip it.
            if (Mathf.Pow((sourceTile.XCoords - destTile.XCoords), 2f) > 1 ||
                Mathf.Pow((sourceTile.YCoords - destTile.YCoords), 2f) > 1)
            {
                continue;;
            }

            // Ensure that an enemy can't move into a tile where an enemy is already present.
            if (destTile.TileOccupier != null)
            {
                UnitBehaviour destTileBehaviour = destTile.TileOccupier.GetComponent<UnitBehaviour>();
                if (destTileBehaviour != null && !destTileBehaviour.Allied)
                {
                    continue;
                }
            }

            // Ensure that an enemy can't move into spawn tiles.
            if (destTile.TileArea == TileAreaType.SPAWN)
            {
                continue;
            }
            
            adjacentTiles.Add(destTile);
        }
        
        WorldTile chosenTile = adjacentTiles[UnityEngine.Random.Range(0, adjacentTiles.Count)];

        if (chosenTile.TileOccupier != null)
        {
            GameObject chosenGameObject = chosenTile.TileOccupier;
            
            AttackUnit(sourceObject, chosenGameObject);
        }

        MoveToTile(sourceObject, sourceTile, chosenTile);
    }
    
    void DisbandUnit()
    {
        //Get selected unit
        //remove unit from game
    }

    void UnitLevelUp()
    {
        //Get unit current level
        //+1
        //check rewards for that level

    }

    void UnitReinforce()
    {
        //Disable movement for unit for 2 turns
        //Increase strength/health for 2 turns
    }
  
}
