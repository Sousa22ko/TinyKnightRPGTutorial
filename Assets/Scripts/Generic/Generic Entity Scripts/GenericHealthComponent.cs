using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public abstract class GenericHealthComponent : MonoBehaviour
{
    protected Entity entity;

    protected float health = 10f;
    protected virtual float maxHealth => 10f;

    protected Vector3 hpBarOffset = new Vector3(0, 0.8f, 0);

    private Slider slider;
    private Transform sliderTransform;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine flashRoutine;

    public float flashDuration = 0.1f;

    protected virtual void Awake()
    {
        health = maxHealth;
        entity = GetComponent<Entity>();

        var sliderGO = Instantiate(Resources.Load<GameObject>("Hp"));
        slider = sliderGO.GetComponentInChildren<Slider>();
        sliderTransform = sliderGO.transform;

        slider.maxValue = maxHealth;
        slider.value = health;
        slider.gameObject.SetActive(health / maxHealth < 1f && health / maxHealth > 0f);


        sliderTransform.SetParent(entity.transform, worldPositionStays: true);
        sliderTransform.localPosition = hpBarOffset;

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;

        UpdateHealthBar();
    }

    protected virtual void Update()
    {
        sliderTransform.localPosition = hpBarOffset;
    }

    public virtual void changeHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);

        FlashDamageFeedback(amount);

        if (health <= 0)
            Destroy(gameObject);

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        var ratio = health / maxHealth;
        slider.value = health;
        slider.gameObject.SetActive(ratio < 1f && ratio > 0f);

    }

    private void FlashDamageFeedback(float amount)
    {
        if (spriteRenderer == null) return;

        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        Color flashColor = amount < 0 ? Color.red : Color.green;
        flashRoutine = StartCoroutine(FlashRoutine(flashColor));
    }

    private IEnumerator FlashRoutine(Color hitColor)
    {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }
}