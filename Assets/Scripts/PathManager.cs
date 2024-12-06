using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public List<Path> paths;

    private void Awake()
    {

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public Vector2 GetDirection(Vector2 position, int indexTo)
    {
        Vector2 to = transform.GetChild(indexTo).transform.position;

        return (to - position).normalized;
    }

    public int GetNextIndex(int indexFrom)
    {
        for (int i = 0; i < paths.Count; ++i)
        {
            if (paths[i].indexFrom == indexFrom)
            {
                return paths[i].GetNextPoint();
            }
        }

        return indexFrom;
    }

    public bool IsArrived(Vector2 position, int indexTo)
    {
        Vector2 dest = transform.GetChild(indexTo).position;

        return (dest - position).sqrMagnitude < 1e-2;
    }

    public Vector3 GetPosition(int index)
    {
        return transform.GetChild(index).position;
    }

    private void OnDrawGizmosSelected()
    {
        if (paths == null || paths.Count == 0)
            return;

        for (int i = 0; i < paths.Count; ++i)
        {
            Gizmos.color = paths[i].pathColor;

            Transform from = transform.GetChild(paths[i].indexFrom);

            for (int j = 0; j < paths[i].indexTo.Count; ++j)
            {
                Transform to = transform.GetChild(paths[i].indexTo[j]);

                Vector3 p0 = from.transform.position;
                Vector3 p1 = to.transform.position;

                float dirLength = 0.0875f;
                float w = 0.707106f; // NOTE: sqrt(2)

                Vector3 center = 0.5f * (p0 + p1);
                Vector3 dir = p1 - p0;
                Vector3 end0 = -dirLength * dir.normalized;

                float xySum = w * (end0.x + end0.y);
                float xySub = w * (end0.x - end0.y);

                Vector3 end1 = new Vector3(xySub, xySum, 0.0f);
                Vector3 end2 = new Vector3(xySum, -xySub, 0.0f);

                Gizmos.DrawLine(p0, p1);
                Gizmos.DrawLine(center, center + end1);
                Gizmos.DrawLine(center, center + end2);
            }
        }
    }
}
