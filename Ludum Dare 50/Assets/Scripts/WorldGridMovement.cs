using UnityEngine;

public class WorldGridMovement : MonoBehaviour
{
    private Camera _mainCamera;

    private Vector3 _dragOrigin;
    private Vector3 _lastDragOrigin;
    
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
            _lastDragOrigin = Input.mousePosition;
        }
        
        if (Input.GetMouseButton(2))
        {
            _dragOrigin = Input.mousePosition;

            Vector3 deltaPos = _dragOrigin - _lastDragOrigin;

            transform.position += (deltaPos * _mouseDragSpeed * Time.deltaTime);

            _lastDragOrigin = _dragOrigin;
        }
    }
}