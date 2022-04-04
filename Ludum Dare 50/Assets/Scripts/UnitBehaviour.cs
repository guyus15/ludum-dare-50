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
            
        //Set selected units coord to the destination tiles coords
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
        else
        {
            return true;
        }
        
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
