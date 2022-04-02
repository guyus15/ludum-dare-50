using UnityEngine;

public class WorldGridMovement : MonoBehaviour
{
    private Camera _mainCamera;
    private float _cameraZDistance;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        _cameraZDistance = _mainCamera.WorldToScreenPoint(transform.position).z;
    }

    private void OnMouseDrag()
    {
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _cameraZDistance);
        Vector3 newWorldPosition = _mainCamera.ScreenToWorldPoint(screenPosition);

        transform.position = newWorldPosition;
    }
}