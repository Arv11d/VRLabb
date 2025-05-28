using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class HPSystem : MonoBehaviour
{
    public int maxHP = 3;
    private int currentHP;

    public AudioSource damageSound;
    public Image vignetteImage;
    public Color damageColor = Color.red;
    public Color deathColor = new Color(0, 0, 0, 0.8f); // Black with transparency

    public float vignetteDuration = 0.5f;      // Duration of damage flash
    public float deathFadeDuration = 2f;       // Duration of death fade
    public string gameOverSceneName = "GameOverScene";
    public MonoBehaviour movementScript;

    private bool isDead = false;

    void Start()
    {
        currentHP = maxHP;
        if (vignetteImage != null)
            vignetteImage.color = Color.clear;
    }

    void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Bottle") || other.CompareTag("EnemySword"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHP -= amount;

        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            if (damageSound != null)
                damageSound.Play();

            if (vignetteImage != null)
                StartCoroutine(FlashVignette(damageColor));
        }
    }

    void Die()
    {
        isDead = true;
        if (movementScript != null)
            movementScript.enabled = false;
        if (damageSound != null)
            damageSound.Play();

        if (vignetteImage != null)
            StartCoroutine(FadeToColor(deathColor, deathFadeDuration));

        Invoke(nameof(LoadGameOverScene), deathFadeDuration + 1f);
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }

    IEnumerator FlashVignette(Color color)
    {
        float halfDuration = vignetteDuration / 2f;
        float time = 0f;

        // Fade in
        while (time < halfDuration)
        {
            float t = time / halfDuration;
            vignetteImage.color = Color.Lerp(Color.clear, color, t);
            time += Time.deltaTime;
            yield return null;
        }

        vignetteImage.color = color;

        // Fade out
        time = 0f;
        while (time < halfDuration)
        {
            float t = time / halfDuration;
            vignetteImage.color = Color.Lerp(color, Color.clear, t);
            time += Time.deltaTime;
            yield return null;
        }

        vignetteImage.color = Color.clear;
    }

    IEnumerator FadeToColor(Color targetColor, float duration)
    {
        Color startColor = vignetteImage.color;
        float time = 0f;

        while (time < duration)
        {
            vignetteImage.color = Color.Lerp(startColor, targetColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        vignetteImage.color = targetColor;
    }
}
