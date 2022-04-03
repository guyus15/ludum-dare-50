using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField] private GameObject _tileMenuObject;
    [SerializeField] private GameObject _generalMenuObject;

    private TileMenu _tileMenu;

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
    }
    #endregion

    private void Start()
    {
        _tileMenu = _tileMenuObject.GetComponent<TileMenu>();

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
        _tileMenu.Population = tile.Population;
        _tileMenu.Income = tile.Income;
        _tileMenu.UnderAttack = tile.UnderAttack;
        _tileMenu.EnemyOwned = tile.EnemyOwned;
        _tileMenu.TurnsUnderAttack = tile.TurnsUnderAttack;
        
        _tileMenu.UpdateMenu();
        
        _tileMenuObject.SetActive(true);
    }

    public void HideTileMenu()
    {
        _tileMenuObject.SetActive(false);

        _currentlySelectedTile = null;
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
