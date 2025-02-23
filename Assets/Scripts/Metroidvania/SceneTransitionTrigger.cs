using UnityEngine;

public class SceneTransitionTrigger : MonoBehaviour
{
    public string targetScene;
    public string targetCheckpoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneLoader.instance.TransitionToScene(targetScene, targetCheckpoint);
        }
    }
}
