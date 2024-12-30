using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    [Header("Room Transition Settings")]
    public RoomCamera.RoomType newRoomType;
    public BoxCollider2D roomBounds;
    public float cameraRelativePosition = 0.5f; // Determines where the camera should start in the new room

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ensure the player is the one triggering the transition
        if (collision.CompareTag("Player") && GameController.instance.currentRoom != transform.parent.gameObject)
        {
            RoomCamera roomCamera = Camera.main.GetComponent<RoomCamera>();

            // use this to trigger transition with different room only
            GameController.instance.currentRoom = transform.parent.gameObject;

            if (roomCamera != null)
            {
                BoxCollider2D boxCollider = roomBounds;

                if (boxCollider != null)
                {
                    roomCamera.cameraRelativePosition = cameraRelativePosition;
                    roomCamera.SetRoomBounds(boxCollider, newRoomType);
                
                }
                else
                {
                    Debug.LogError("RoomTransition requires a BoxCollider2D component to determine bounds.");
                }
            }
            else
            {
                Debug.LogError("Main Camera is missing RoomCamera component.");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        if (boxCollider != null)
        {
            Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
        }
    }
}
