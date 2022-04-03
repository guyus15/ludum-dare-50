using UnityEngine;
using UnityEngine.UI;

public class GeneralMenu : MonoBehaviour
{
    public int TotalPopulation { private get; set; }
    public int TotalIncome { private get; set; }
    public int AreasUnderAttack { private get; set; }
    public int ControlledAreas { private get; set; }
    public int EnemyOwnedAreas { private get; set; }

    [SerializeField] private Text _totalPopulation;
    [SerializeField] private Text _totalIncomeText;
    [SerializeField] private Text _areasUnderAttackText;
    [SerializeField] private Text _controlledAreasText;
    [SerializeField] private Text _enemyOwnedAreas;

    public void UpdateMenu()
    {
        _totalPopulation.text = "Total Population: " + TotalPopulation;
        _totalIncomeText.text = "Total Income: " + TotalIncome;
        _areasUnderAttackText.text = "Areas Under Attack: " + AreasUnderAttack;
        _controlledAreasText.text = "Controller Areas: " + ControlledAreas;
        _enemyOwnedAreas.text = "Enemy Owned Areas: " + EnemyOwnedAreas;
    }
}
