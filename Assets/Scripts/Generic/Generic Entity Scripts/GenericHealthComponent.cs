using UnityEngine;
using UnityEngine.UI;

public abstract class GenericHealthComponent : MonoBehaviour
{

    protected Entity entity;
    public GameObject hpBarPrefab;
    private GameObject hpBarInstance;
    protected Vector3 hpBarOffset = new Vector3(0, 0.8f, 0);

    [SerializeField] private Image fillImage;
    [SerializeField] private Canvas canvas;

    protected float health = 10f;
    protected virtual float maxHealth => 10f;

    protected virtual void Awake()
    {
        health = maxHealth;

        entity = GetComponent<Entity>();
        if (hpBarPrefab == null)
        {
            hpBarPrefab = Resources.Load<GameObject>("Hp Bar");
            hpBarPrefab.transform.localPosition = Vector3.zero;
        }


        hpBarInstance = Instantiate(hpBarPrefab, transform.position + hpBarOffset, Quaternion.identity, transform);
        canvas = hpBarInstance.GetComponentInChildren<Canvas>();


        hpBarInstance.transform.localPosition = hpBarOffset;
        canvas.transform.localPosition = Vector2.zero;

        fillImage = canvas.transform.Find("background/hp").GetComponent<Image>();
        UpdateHealthBar();
    }

    public virtual void changeHealth(float amount)
    {
        health += amount;

        if (health <= 0)
            Destroy(gameObject);

        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        var ratio = health / maxHealth;
        fillImage.fillAmount = ratio;
        canvas.enabled = ratio < 1f && ratio > 0f;
    }

}