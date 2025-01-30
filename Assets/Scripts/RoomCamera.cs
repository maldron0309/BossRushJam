using System.Collections;
using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    public static RoomCamera instance;
    public enum RoomType { Horizontal, Vertical, SingleScreen }

    [Header("Room Settings")]
    public RoomType roomType;
    public Transform player;
    public float leftBound;
    public float rightBound;
    public float topBound;
    public float bottomBound;

    [Header("Camera Settings")]
    [Range(0f, 1f)]
    public float cameraRelativePosition = 0.5f; // Determines camera position for Horizontal and Vertical rooms
    public Vector2 cameraOffset; // Offset for camera position if needed
    public float transitionDuration = 1.0f; // Time taken to transition to new room bounds

    private Camera mainCamera;
    private Vector2 cameraBounds; // Half size of the camera view in world units
    private bool isTransitioning;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float transitionCounter;
    private bool isShaking = false;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //leftBound += transform.position.x;
        //rightBound += transform.position.x;
        //topBound += transform.position.y;
        //bottomBound += transform.position.y;
        mainCamera = Camera.main;
        CalculateCameraBounds();
        PositionCamera();
    }

    void LateUpdate()
    {
        if (isShaking)
            return;

        if (isTransitioning)
        {
            SmoothTransition();
        }
        else if (player != null)
        {
            switch (roomType)
            {
                case RoomType.Horizontal:
                    FollowPlayerHorizontal();
                    break;
                case RoomType.Vertical:
                    FollowPlayerVertical();
                    break;
                case RoomType.SingleScreen:
                    CenterCamera();
                    break;
            }
        }
    }

    private void CalculateCameraBounds()
    {
        float cameraHeight = mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        cameraBounds = new Vector2(cameraWidth / 2, cameraHeight / 2);
    }

    private void PositionCamera()
    {
        targetPosition = transform.position;

        switch (roomType)
        {
            case RoomType.Horizontal:
                targetPosition.x = Mathf.Lerp(leftBound + cameraBounds.x,
                                              rightBound - cameraBounds.x,
                                              cameraRelativePosition);
                targetPosition.y = (topBound + bottomBound) / 2;
                break;
            case RoomType.Vertical:
                targetPosition.x = (leftBound + rightBound) / 2;
                targetPosition.y = Mathf.Lerp(topBound - cameraBounds.y,
                                              bottomBound + cameraBounds.y,
                                              cameraRelativePosition);
                break;
            case RoomType.SingleScreen:
                targetPosition = new Vector3((leftBound + rightBound) / 2, (topBound + bottomBound) / 2, transform.position.z);
                break;
        }

        transform.position = targetPosition + (Vector3)cameraOffset;
    }

    private void FollowPlayerHorizontal()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Clamp(player.position.x,
                                       leftBound + cameraBounds.x,
                                       rightBound - cameraBounds.x);
        transform.position = new Vector3(currentPosition.x, (topBound + bottomBound) / 2, transform.position.z) + (Vector3)cameraOffset;
        cameraRelativePosition = (player.position.x) / (rightBound - cameraBounds.x);
    }

    private void FollowPlayerVertical()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.y = Mathf.Clamp(player.position.y,
                                       bottomBound + cameraBounds.y,
                                       topBound - cameraBounds.y);
        transform.position = new Vector3((leftBound + rightBound) / 2, currentPosition.y, transform.position.z) + (Vector3)cameraOffset;
    }

    private void CenterCamera()
    {
        transform.position = new Vector3((leftBound + rightBound) / 2, (topBound + bottomBound) / 2, transform.position.z) + (Vector3)cameraOffset;
    }

    public void SetRoomBounds(BoxCollider2D roomBounds, RoomType newRoomType)
    {
        leftBound = roomBounds.bounds.min.x;
        rightBound = roomBounds.bounds.max.x;
        topBound = roomBounds.bounds.max.y;
        bottomBound = roomBounds.bounds.min.y;
        roomType = newRoomType;

        StartTransition();
    }

    private void StartTransition()
    {
        isTransitioning = true;
        transitionCounter = 0;
        startPosition = transform.position;
        CalculateCameraBounds();

        switch (roomType)
        {
            case RoomType.Horizontal:
                targetPosition.x = Mathf.Lerp(leftBound + cameraBounds.x,
                                              rightBound - cameraBounds.x,
                                              cameraRelativePosition);
                targetPosition.y = (topBound + bottomBound) / 2;
                break;
            case RoomType.Vertical:
                targetPosition.x = (leftBound + rightBound) / 2;
                targetPosition.y = Mathf.Lerp(topBound - cameraBounds.y,
                                              bottomBound + cameraBounds.y,
                                              cameraRelativePosition);
                break;
            case RoomType.SingleScreen:
                targetPosition = new Vector3((leftBound + rightBound) / 2, (topBound + bottomBound) / 2, transform.position.z);
                break;
        }
    }

    private void SmoothTransition()
    {
        if (isShaking)
            return;

        transitionCounter += Time.deltaTime;
        transform.position = Vector3.Lerp(startPosition, targetPosition + (Vector3)cameraOffset, transitionCounter / transitionDuration);

        if (Vector3.Distance(transform.position, targetPosition + (Vector3)cameraOffset) < 0.01f)
        {
            transform.position = targetPosition + (Vector3)cameraOffset;
            isTransitioning = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(leftBound, topBound, 0), new Vector3(rightBound, topBound, 0));
        Gizmos.DrawLine(new Vector3(rightBound, topBound, 0), new Vector3(rightBound, bottomBound, 0));
        Gizmos.DrawLine(new Vector3(rightBound, bottomBound, 0), new Vector3(leftBound, bottomBound, 0));
        Gizmos.DrawLine(new Vector3(leftBound, bottomBound, 0), new Vector3(leftBound, topBound, 0));
    }

    public void Shake(float duration, float intensity)
    {
        if (!isShaking);
            StartCoroutine(ShakeCoroutine(duration, intensity));
    }
    private IEnumerator ShakeCoroutine(float duration, float intensity)
    {
        float elapsed = 0f;
        Vector3 originalPosition = transform.position;
        isShaking = true;

        while (elapsed < duration)
        {
            // Random offset in X and Y
            float offsetX = Random.Range(-1f, 1f) * intensity;
            float offsetY = Random.Range(-1f, 1f) * intensity;

            transform.position = originalPosition + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset camera position
        transform.position = originalPosition;
        isShaking = false;
    }
}
