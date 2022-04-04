using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera _camera;
    
    private Vector3 _dragOrigin;
    private Vector3 _lastDragOrigin;
    
    [Header("Mouse drag movement")]
    [SerializeField] private float _mouseDragSpeed;
    
    [Header("Mouse zoom movement")]
    [SerializeField] private float _minCameraZoom = 5f;
    [SerializeField] private float _maxCameraZoom = 0.5f;
    [SerializeField] private float _cameraZoomStep = 0.5f;

    private Vector3 _startingPosition;
    
    private void Start()
    {
        _camera = GetComponent<Camera>();

        _startingPosition = transform.position;
    }
    
    private void Update()
    {
        // Camera dragging movement
        
        float proportionateDragSpeed = (_mouseDragSpeed / _minCameraZoom) * _camera.orthographicSize;
        
        if (Input.GetMouseButtonDown(2))
        {
            _lastDragOrigin = Input.mousePosition;
        }
        
        if (Input.GetMouseButton(2))
        {
            _dragOrigin = Input.mousePosition;

            Vector3 deltaPos = _lastDragOrigin - _dragOrigin;

            transform.position += (deltaPos * proportionateDragSpeed * Time.deltaTime);

            int gridLength = WorldGrid.instance.GetGridSize();

            float xPos = transform.position.x;
            float yPos = transform.position.y;
            
            if (transform.position.x > _startingPosition.x + (gridLength / 2) - 0.01)
            {
                transform.position = new Vector3(
                    _startingPosition.x + (gridLength / 2),
                    transform.position.y,
                    transform.position.z
                );
            } else if (transform.position.x < _startingPosition.x - (gridLength / 2) + 0.01)
            {
                transform.position = new Vector3(
                    _startingPosition.x - (gridLength / 2),
                    transform.position.y,
                    transform.position.z
                );
            }

            if (transform.position.y > _startingPosition.y + (gridLength / 2) + 0.01)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    _startingPosition.y + (gridLength / 2),
                    transform.position.z
                );
            } else if (transform.position.y < _startingPosition.y - (gridLength / 2) - 0.01)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    _startingPosition.y - (gridLength / 2),
                    transform.position.z
                );
            }

            _lastDragOrigin = _dragOrigin;
        }

        // Camera zooming
        
        if (Input.mouseScrollDelta.y < 0)
        {
            _camera.orthographicSize += _cameraZoomStep;
        } else if (Input.mouseScrollDelta.y > 0)
        {
            _camera.orthographicSize -= _cameraZoomStep;
        }
        
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, _maxCameraZoom, _minCameraZoom);
    }
}
