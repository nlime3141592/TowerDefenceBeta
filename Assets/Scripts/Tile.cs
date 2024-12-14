using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isSelected = false;

    public Tower havingTower = null;

    private SpriteRenderer rnd;
    private BoxCollider col;

    private void Awake()
    {
        rnd = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (havingTower == null)
        {
            if (isSelected)
                rnd.color = Color.white;
            else
                rnd.color = new Color(0.75f, 0.75f, 0.75f, 1.0f);

            rnd.enabled = true;
            col.size = new Vector3(0.7f, 0.25f, 0.2f);
            col.center = Vector3.zero;
        }
        else
        {
            rnd.enabled = false;
            col.center = Vector3.up * 0.25f;
            col.size = new Vector3(0.7f, 0.75f, 0.15f);
        }
    }

    public void PrintName()
    {
        Debug.Log(this.gameObject.name);
    }
}
