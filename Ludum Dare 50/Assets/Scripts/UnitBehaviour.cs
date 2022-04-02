using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    private float _unitHealth;
    private int _unitStrength;
    private int _unitLevel;
    private int _unitMoveSpeed;
    private int _unitUpkeep;
    private int _unitCost;
    private int[,] _unitPosition;
    //UNIT TYPE
    //PREFERRED TILE

    //Movement
    //Attacking 
    //Spawn
    //Disband
    //Level up
    //reinforce

    void MoveToTile()
    {
        //While unit is selected
        //Get tile related to mouse position
        //set Units coords to that tile
    }

    void AttackLand()
    {
        //Get current position
        //Get Land status (health)
        //Remove 1 from the land status
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
