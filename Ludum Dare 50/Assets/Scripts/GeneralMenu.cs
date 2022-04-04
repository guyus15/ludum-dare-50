using UnityEngine;
using UnityEngine.UI;

public class GeneralMenu : MonoBehaviour
{
    public int TotalIncome { private get; set; }
    public int ControlledAreas { private get; set; }
    public int EnemyOwnedAreas { private get; set; }
    
    [SerializeField] private Text _totalIncomeText;
    [SerializeField] private Text _areasUnderAttackText;
    [SerializeField] private Text _controlledAreasText;
    [SerializeField] private Text _enemyOwnedAreas;

    public void UpdateMenu()
    {
        _totalIncomeText.text = "Total Income: " + TotalIncome;
        _controlledAreasText.text = "Controlled Areas: " + ControlledAreas;
        _enemyOwnedAreas.text = "Enemy Owned Areas: " + EnemyOwnedAreas;
    }
}
