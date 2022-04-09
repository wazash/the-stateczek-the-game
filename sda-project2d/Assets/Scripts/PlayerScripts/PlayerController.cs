using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private float speed;

    private Camera activeCamera;
    private Rect cameraBounds;

    private void Start()
    {
        activeCamera = Camera.main;

        // Get left bottom camera corner
        Vector3 bottomLeftPosition = activeCamera.ScreenToWorldPoint(Vector3.zero);
        // Get get right top camera corner
        Vector3 topRightPosition = activeCamera.ScreenToWorldPoint(new Vector3(activeCamera.pixelWidth, activeCamera.pixelHeight, 0));

        cameraBounds = new Rect(
            bottomLeftPosition.x, 
            bottomLeftPosition.y, 
            topRightPosition.x - bottomLeftPosition.x, 
            topRightPosition.y - bottomLeftPosition.y);
    }

    private void FixedUpdate()
    {
        Vector2 movementVector = new Vector2(
            inputManager.HorizontalInput * speed, 
            inputManager.VerticalInput * speed);

        rigidbody.AddForce(movementVector);
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, cameraBounds.xMin, cameraBounds.xMax),
            Mathf.Clamp(transform.position.y, cameraBounds.yMin, cameraBounds.yMax),
            transform.position.z);
    }
}
