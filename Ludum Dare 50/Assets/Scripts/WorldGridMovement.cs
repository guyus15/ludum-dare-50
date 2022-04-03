using UnityEngine;

public class WorldGridMovement : MonoBehaviour
{
    private Camera _mainCamera;

    private Vector3 _dragOrigin;
    
    [SerializeField] private float _mouseDragSpeed;
    
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            _dragOrigin = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 pos = _mainCamera.ScreenToViewportPoint(Input.mousePosition - _dragOrigin);
            Vector3 move = new Vector3(pos.x * _mouseDragSpeed, pos.y * _mouseDragSpeed, 0);
            
            transform.Translate(move, Space.World);
        }
    }
}