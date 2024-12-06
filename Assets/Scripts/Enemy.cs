using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 0.45f;
    public float health = 1.0f;

    public float bodyAmp = 0.03f;
    public float bodyBias = 0.0f;
    public float bodySpeed = 1.5f;

    public PathManager pathManager;
    public int pathTo;

    public Vector3 direction;

    private SpriteRenderer sprnd;

    private void Awake()
    {
        sprnd = transform.Find("Sprites/Body").GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        this.direction = pathManager.GetDirection(transform.position, pathTo);
        transform.position += moveSpeed * Time.fixedDeltaTime * this.direction;

        if (pathManager.IsArrived(transform.position, pathTo))
        {
            pathTo = pathManager.GetNextIndex(pathTo);
        }
    }

    private void Update()
    {
        VibBody();
    }

    private void VibBody()
    {
        float y = Mathf.Sin(Time.time * bodySpeed) * bodyAmp + bodyBias;
        sprnd.transform.localPosition = new Vector3(0.0f, y, 0.0f);
    }
}