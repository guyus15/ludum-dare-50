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

    void AttackLand()
    {
        //Get current position
        //Get Land status (health)
        //Remove 1 from the land status
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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
