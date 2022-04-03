using UnityEngine;

public class WorldGridMovement : MonoBehaviour
{
    private Camera _mainCamera;

    private Vector3 _dragOrigin;
    private Vector3 _lastDragOrigin;

    [Header("Mouse drag movement")]
    [SerializeField] private float _mouseDragSpeed;
    
    [Header("Mouse zoom movement")]
    [SerializeField] private float _minCameraZoom = 5f;
    [SerializeField] private float _maxCameraZoom = 0.5f;
    [SerializeField] private float _cameraZoomStep = 0.5f;
    
    private void Start()
    {
        _mainCamera = Camera.main;

        _dragOrigin = Vector3.zero;
    }

    private void Update()
    {
        // Camera dragging movement

        float proportionateDragSpeed = (_mouseDragSpeed / _minCameraZoom) * _mainCamera.orthographicSize;
        
        if (Input.GetMouseButtonDown(2))
        {
            _lastDragOrigin = Input.mousePosition;
        }
        
        if (Input.GetMouseButton(2))
        {
            _dragOrigin = Input.mousePosition;

            Vector3 deltaPos = _dragOrigin - _lastDragOrigin;

            transform.position += (deltaPos * proportionateDragSpeed * Time.deltaTime);

            _lastDragOrigin = _dragOrigin;
        }

        // Camera zooming
        
        if (Input.mouseScrollDelta.y < 0)
        {
            _mainCamera.orthographicSize += _cameraZoomStep;
        } else if (Input.mouseScrollDelta.y > 0)
        {
            _mainCamera.orthographicSize -= _cameraZoomStep;
        }

        if (_mainCamera.orthographicSize > _minCameraZoom)
        {
            _mainCamera.orthographicSize = _minCameraZoom;
        } else if (_mainCamera.orthographicSize < _maxCameraZoom)
        {
            _mainCamera.orthographicSize = _maxCameraZoom;
        }
    }
}