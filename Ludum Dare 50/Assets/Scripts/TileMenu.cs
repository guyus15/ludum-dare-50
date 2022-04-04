using UnityEngine;
using UnityEngine.UI;

public class TileMenu : MonoBehaviour
{
    public string AreaName { get; set; }
    public int Income { get; set; }
    public bool UnderAttack { get; set; }
    public bool EnemyOwned { get; set; }
    public int TurnsUnderAttack { get; set; }
    public bool Occupied { get; set; }

    [SerializeField] private Text _areaNameText;
    [SerializeField] private Text _incomeText;
    [SerializeField] private Text _underAttackText;
    [SerializeField] private Text _enemyOwnedText;
    [SerializeField] private Text _turnsUnderAttackText;
    
    public void UpdateMenu()
    {
        _areaNameText.text = AreaName;
        _incomeText.text = "Income: " + (Occupied ? Income.ToString() : "0");
        _underAttackText.text = "Under Attack: " + (UnderAttack ? "Yes" : "No");
        _enemyOwnedText.text = "Enemy Owned: " + (EnemyOwned ? "Yes" : "No");
        _turnsUnderAttackText.text = "Turns Under Attack: " + TurnsUnderAttack.ToString();
    }
}
