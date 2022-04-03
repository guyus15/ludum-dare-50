using UnityEngine;

public class WorldGridMovement : MonoBehaviour
{
    private Camera _mainCamera;

    private Vector3 _dragOrigin;
    
    [SerializeField] private float _mouseDragSpeed;
    
    private void Start()
    {
        _mainCamera = Camera.main;

        _dragOrigin = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            _dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 pos = _mainCamera.ScreenToViewportPoint(_dragOrigin - Input.mousePosition);
            Vector3 move = new Vector3(pos.x * _mouseDragSpeed, pos.y * _mouseDragSpeed, 0);

            Vector3 deltaPos = transform.position - move;
            
            transform.position = deltaPos;
        }
    }
}