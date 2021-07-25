using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColliderElementManager : MonoBehaviour
{
    public EdgeCollider2D copyCollider; // 카피할 콜라이더
    public PolygonCollider2D targetCollider; // 카피당할 콜라이더
    
    public void CopyElement()
    {
        targetCollider.points = new Vector2[copyCollider.pointCount];

        targetCollider.SetPath(0, copyCollider.points);
    }

    public void AddElement()
    {
        // targetCollider.points = new Vector2[targetCollider.points.Length + copyCollider.pointCount];
        // targetCollider.SetPath(targetCollider.pathCount, copyCollider.points);
    }

}
