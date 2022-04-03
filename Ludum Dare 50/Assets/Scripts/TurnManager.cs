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
    
    public void NextTurn()
    {
        if (CurrentTurn > 0)
        {
            // Spawn enemies
        }

        // Update tile values based on their current properties.
        
        // Update player currency.
        
        // Enemy movement/attacks

        CurrentTurn++;
    }
}
