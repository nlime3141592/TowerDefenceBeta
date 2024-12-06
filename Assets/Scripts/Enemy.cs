using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;

    public PathManager pathManager;
    public int pathTo;

    public Vector3 direction;

    private void FixedUpdate()
    {
        Vector3 direction = pathManager.GetDirection(transform.position, pathTo);
        transform.position += moveSpeed * Time.fixedDeltaTime * direction;

        if (pathManager.IsArrived(transform.position, pathTo))
        {
            pathTo = pathManager.GetNextIndex(pathTo);
        }
    }

    private void Update()
    {

    }
}