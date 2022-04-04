using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    
    public int CurrentTurn { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Debug.Log("Instance already exists. Deleting object.");
            Destroy(this);
        }
    }

    private void Start()
    {
        MenuManager.instance.UpdateGeneralMenu();   
    }
    
    public void NextTurn()
    {
        WorldGrid.instance.SpawnEnemies();
        
        // Update tile values based on their current properties.
        WorldGrid.instance.UpdateWorldValues();
        
        // Update the general menu.
        MenuManager.instance.UpdateGeneralMenu();
        
        
        // Enemy movement/attacks

        CurrentTurn++;
    }
}
