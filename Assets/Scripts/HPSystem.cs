using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class HPSystem : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHP = 3;
    private int currentHP;

    [Header("Audio")]
    public AudioSource damageSound;

    [Header("Vignette UI")]
    public Image vignetteImage;
    public Color damageColor = Color.red;
    public Color deathColor = new Color(0, 0, 0, 0.8f);

    [Header("Damage Vignette Settings")]
    public float damageFadeInDuration = 0.2f;
    public float damageFadeOutDuration = 0.3f;

    [Header("Death Vignette Settings")]
    public float deathFadeDuration = 2f;

    [Header("Other Settings")]
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
                StartCoroutine(FlashVignette(damageColor, damageFadeInDuration, damageFadeOutDuration));
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

    IEnumerator FlashVignette(Color color, float fadeInDuration, float fadeOutDuration)
    {
        // Fade in
        float time = 0f;
        while (time < fadeInDuration)
        {
            float t = time / fadeInDuration;
            vignetteImage.color = Color.Lerp(Color.clear, color, t);
            time += Time.deltaTime;
            yield return null;
        }
        vignetteImage.color = color;

        // Fade out
        time = 0f;
        while (time < fadeOutDuration)
        {
            float t = time / fadeOutDuration;
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
