using System.Collections;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    [Header("Room Transition Settings")]
    public RoomCamera.RoomType newRoomType;
    public BoxCollider2D roomBounds;
    public float cameraRelativePosition = 0.5f; // Determines where the camera should start in the new room
    public bool keepMomentum = false;
    public bool isInstant = false;

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
                    roomCamera.SetRoomBounds(boxCollider, newRoomType, !isInstant);
                    if (!isInstant)
                        StartCoroutine(HoldPlayer(collision.GetComponent<PlayerController>()));
                    else
                    {
                        
                    }
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
        if(roomBounds != null)
            Gizmos.DrawWireCube(roomBounds.bounds.center, roomBounds.bounds.size);
    }
    private IEnumerator HoldPlayer(PlayerController player)
    {
        Vector2 momentum = player.GetComponent<Rigidbody2D>().velocity;
        player.Stop();
        yield return new WaitForSeconds(1);
        player.Resume();
        if (keepMomentum)
            player.GetComponent<Rigidbody2D>().velocity = momentum;
    }
}
