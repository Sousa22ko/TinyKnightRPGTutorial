using UnityEngine;

public static class Util
{

    public static void copyOrderInLayer(SpriteRenderer from, SpriteRenderer to)
    {
        if (from == null || to == null) return;

        to.sortingLayerID = from.sortingLayerID;
        to.sortingOrder = from.sortingOrder;
    }

}