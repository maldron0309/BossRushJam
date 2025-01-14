using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseBossController : MonoBehaviour
{
    public bool facingRight = true;
    public bool isBattleStarted = false;
    [SerializeField] private GameObject door;
    public GameObject model;
    void Start()
    {
    }

    public void StartBossBattle()
    {
        isBattleStarted = true;
    }
    
    public void ActivateDoor()
    {
        if (door != null)
        {
            door.SetActive(true);
        }
    }
}
    public bool FacinRight()
    {
        return facingRight;
    }
    private void Flip()
    {
        facingRight = !facingRight;

        // Flip the sprite by inverting the local scale's X value
        Vector3 scale = model.transform.localScale;
        scale.x *= -1;
        model.transform.localScale = scale;
    }
    public void FacePlayer()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();
        if ((transform.position.x > player.transform.position.x && facingRight) || (transform.position.x < player.transform.position.x && !facingRight))
        {
            Flip();
        }

    }
}
