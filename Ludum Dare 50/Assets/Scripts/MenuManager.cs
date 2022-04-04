using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField] private GameObject _tileMenuObject;
    [SerializeField] private GameObject _generalMenuObject;

    private TileMenu _tileMenu;
    private GeneralMenu _generalMenu;

    private WorldTile _currentlySelectedTile;
    
    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
        
        // Initialising menu values
        _tileMenu = _tileMenuObject.GetComponent<TileMenu>();
        _generalMenu = _generalMenuObject.GetComponent<GeneralMenu>();
    }
    #endregion

    private void Start()
    {
        _tileMenuObject.SetActive(false);
        _generalMenuObject.SetActive(true);

        _currentlySelectedTile = null;
    }
    
    public void ShowTileMenu(WorldTile tile)
    {
        if (_currentlySelectedTile != null && _currentlySelectedTile.Equals(tile))
        {
            HideTileMenu();
            return;
        }
        
        _currentlySelectedTile = tile;
        
        _tileMenu.AreaName = tile.TileArea.ToString();
        _tileMenu.Income = tile.Income;
        _tileMenu.UnderAttack = tile.UnderAttack;
        _tileMenu.EnemyOwned = tile.EnemyOwned;
        _tileMenu.TurnsUnderAttack = tile.TurnsUnderAttack;
        _tileMenu.Occupied = tile.TileOccupier != null;
        
        _tileMenu.UpdateMenu();
        
        _tileMenuObject.SetActive(true);
    }

    public void HideTileMenu()
    {
        _tileMenuObject.SetActive(false);

        _currentlySelectedTile = null;
    }

    public void UpdateGeneralMenu()
    {
        _generalMenu.TotalIncome = WorldGrid.instance.GridIncome;
        _generalMenu.ControlledAreas = WorldGrid.instance.GridControlledAreas;
        _generalMenu.EnemyOwnedAreas = WorldGrid.instance.GridEnemyOwnedAreas;
        
        _generalMenu.UpdateMenu();
    }
    
    public void ShowGeneralMenu()
    {
        _generalMenuObject.SetActive(true);
    }

    public void HideGeneralMenu()
    {
        _generalMenuObject.SetActive(false);
    }
}
