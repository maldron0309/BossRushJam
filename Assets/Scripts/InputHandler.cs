using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public PlayerController playerController; // Reference to the PlayerController script

    // Sensitivity for joystick dead zones
    [Range(0.1f, 1f)] public float joystickDeadZone = 0.2f;

    void Update()
    {
        if (!playerController.isInputEnabled)
        {
            return;
        }
        float moveInput = Input.GetAxis("Horizontal"); // Supports keyboard and gamepad
        if (Mathf.Abs(moveInput) > joystickDeadZone)
        {
            playerController.OnMove(moveInput);
        }
        else
        {
            playerController.OnMove(0f);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q");
        }

        // Handle jump input
        if (Input.GetButtonDown("Jump"))
        {
            playerController.OnJump(true);
        }
        // Handle jump input
        if (Input.GetButtonUp("Jump"))
        {
            playerController.OnJump(false);
        }

        // Handle attack input
        if (Input.GetButtonDown("Fire1"))
        {
            playerController.OnAttack1(true); // Regular attack
        }

        // Handle charge attack (if you want to support it)
        if (Input.GetButtonUp("Fire1"))
        {
            playerController.OnAttack1(false); // Charging attack
        }
        // Handle attack input
        if (Input.GetButtonDown("Dash"))
        {
            playerController.PerformDodge(); // Regular attack
        }

    }
}
