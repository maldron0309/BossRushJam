using UnityEngine;

public class AfterImage : MonoBehaviour
{
    public float duration = 0.5f;  // Time before the afterimage fades out
    public float fadeSpeed = 2f;   // Speed of the fade-out effect

    private SpriteRenderer spriteRenderer;
    private Color startColor;
    private float timeElapsed = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        float alpha = Mathf.Lerp(0.5f, 0, timeElapsed / duration);  // Gradually fade out
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

        if (timeElapsed >= duration)
        {
            Destroy(gameObject);  // Remove the afterimage when fully faded
        }
    }
}
