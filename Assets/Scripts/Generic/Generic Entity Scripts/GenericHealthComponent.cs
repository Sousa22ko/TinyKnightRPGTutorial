using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public abstract class GenericHealthComponent : MonoBehaviour
{
    protected Entity entity;

    protected float health = 10f;
    protected virtual float maxHealth => 10f;
    protected virtual float autoHeal => 0f;
    protected virtual float continuousDamage => 0f;

    protected Vector3 hpBarOffset = new Vector3(0, 0.8f, 0);

    private Slider slider;
    private Transform sliderTransform;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private Coroutine flashRoutine;
    private Coroutine autoChangeHealthRoutine;

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
        if ((autoHeal > 0 || continuousDamage > 0) && autoChangeHealthRoutine == null)
        {
            autoChangeHealthRoutine = StartCoroutine(AutoChangeHealthRoutine());
        }

        sliderTransform.localScale = new Vector3(Mathf.Sign(entity.transform.localScale.x), sliderTransform.localScale.y, sliderTransform.localScale.z);
    }

    public virtual void changeHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);

        FlashDamageFeedback(amount);

        if (health <= 0)
        {
            GameObject skullPrefab = Resources.Load<GameObject>("Skull");
            if (skullPrefab != null)
            {
                GameObject skull = Instantiate(skullPrefab, transform.position, Quaternion.identity);

                Util.copyOrderInLayer(entity.transform.GetComponent<SpriteRenderer>(), skull.GetComponent<SpriteRenderer>());

                GameObject skullsContainer = GameObject.Find("Skulls");
                if (skullsContainer != null)
                    skull.transform.parent = skullsContainer.transform;
            }
            Destroy(gameObject);
        }

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

    private IEnumerator AutoChangeHealthRoutine()
    {
        while (true)
        {
            bool shouldHeal = autoHeal > 0 && health < maxHealth;
            bool shouldDamage = continuousDamage > 0;

            if (!shouldHeal && !shouldDamage)
                break;

            float delta = 0f;

            if (shouldHeal)
                delta += autoHeal;

            if (shouldDamage)
                delta -= continuousDamage;

            if (delta != 0)
                changeHealth(delta);

            yield return new WaitForSeconds(0.5f);
        }

        autoChangeHealthRoutine = null;
    }
}