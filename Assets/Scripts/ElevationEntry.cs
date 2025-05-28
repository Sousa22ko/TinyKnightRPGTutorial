using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public enum ElevationDirection { Up, Down }

public class ElevationEntry : MonoBehaviour
{
    public ElevationDirection direction;

    public Collider2D[] elevationColliders;
    public Collider2D[] elevationBoundryColliders;

    public void OnTriggerExit2D(Collider2D entity)
    {

        var flip = entity.transform.position.y > transform.position.y;


        foreach (Collider2D mountain in elevationColliders)
        {
            Physics2D.IgnoreCollision(entity, mountain, flip);
        }

        foreach (Collider2D boundry in elevationBoundryColliders)
        {
            Physics2D.IgnoreCollision(entity, boundry, !flip);
        }

        entity.gameObject.GetComponent<SpriteRenderer>().sortingOrder = elevationColliders.Max(c => c.gameObject.GetComponent<TilemapRenderer>().sortingOrder) + (flip ? 1 : -1);
    }

}
