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

    [Header("UI Elements")]
    public Image damageFlashImage;
    public Image deathScreenImage; // Set this to a black image with 0 alpha in the editor

    [Header("Damage Flash Settings")]
    public Color damageColor = Color.red;
    public float damageFadeInDuration = 0.2f;
    public float damageFadeOutDuration = 0.3f;

    [Header("Death Screen Settings")]
    public float deathFadeDuration = 2f;

    [Header("Other Settings")]
    public string gameOverSceneName = "GameOverScene";
    public MonoBehaviour movementScript;

    private bool isDead = false;

    void Start()
    {
        currentHP = maxHP;

        if (damageFlashImage != null)
            damageFlashImage.color = Color.clear;

        if (deathScreenImage != null)
        {
            Color c = deathScreenImage.color;
            c.a = 0f;
            deathScreenImage.color = c;
        }
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

            if (damageFlashImage != null)
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

        if (deathScreenImage != null)
            StartCoroutine(FadeInDeathScreen(deathFadeDuration));

        Invoke(nameof(LoadGameOverScene), deathFadeDuration + 1f);
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }

    IEnumerator FlashVignette(Color color, float fadeInDuration, float fadeOutDuration)
    {
        float time = 0f;
        while (time < fadeInDuration)
        {
            float t = time / fadeInDuration;
            damageFlashImage.color = Color.Lerp(Color.clear, color, t);
            time += Time.deltaTime;
            yield return null;
        }
        damageFlashImage.color = color;

        time = 0f;
        while (time < fadeOutDuration)
        {
            float t = time / fadeOutDuration;
            damageFlashImage.color = Color.Lerp(color, Color.clear, t);
            time += Time.deltaTime;
            yield return null;
        }
        damageFlashImage.color = Color.clear;
    }

    IEnumerator FadeInDeathScreen(float duration)
    {
        float time = 0f;
        Color color = deathScreenImage.color;

        while (time < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, time / duration);
            color.a = alpha;
            deathScreenImage.color = color;
            time += Time.deltaTime;
            yield return null;
        }

        color.a = 1f;
        deathScreenImage.color = color;
    }
}
