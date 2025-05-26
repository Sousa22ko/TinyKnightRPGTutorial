using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public enum ElevationDirection { Up, Down }

public class ElevationEntry : MonoBehaviour
{
    public ElevationDirection direction;

    public Collider2D[] elevationColliders;
    public Collider2D[] elevationBoundryColliders;

    // inicialmente desativa a colisão com boundrys
    public void Awake()
    {
        var entities = GameObject.FindGameObjectsWithTag("Player").Concat(GameObject.FindGameObjectsWithTag("Enemy"));

        foreach (GameObject entity in entities)
        {
            Collider2D entityCollider = entity.GetComponent<Collider2D>();
            if (entityCollider == null) continue;

            foreach (Collider2D boundry in elevationBoundryColliders)
            {
                Physics2D.IgnoreCollision(entityCollider, boundry, true);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D entity)
    {

        //var rb = entity.attachedRigidbody;
        //if (rb != null && rb.linearVelocity.y > 0)
        //    direction = ElevationDirection.Up;
        //else
        //    direction = ElevationDirection.Down;

        var flip = entity.transform.position.y > transform.position.y;


        foreach (Collider2D mountain in elevationColliders)
        {
            //mountain.enabled = !flip;
            Physics2D.IgnoreCollision(entity, mountain, flip);
        }

        foreach (Collider2D boundry in elevationBoundryColliders)
        {
            //boundry.enabled = flip;
            Physics2D.IgnoreCollision(entity, boundry, !flip);
        }

        entity.gameObject.GetComponent<SpriteRenderer>().sortingOrder = elevationColliders.Max(c => c.gameObject.GetComponent<TilemapRenderer>().sortingOrder) + (flip ? 1 : -1);
    }

}
