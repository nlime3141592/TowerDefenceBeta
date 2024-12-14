using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 0.45f;
    public float health = 1.0f;
    public int rewardMoney = 0;

    public float bodyAmp = 0.03f;
    public float bodyBias = 0.0f;
    public float bodySpeed = 1.5f;

    public List<DestroyPoint> destroyPoints;
    public PathManager pathManager;
    public int pathTo;

    public Vector3 direction;

    private SpriteRenderer sprnd;
    private bool shouldDestroyForce;

    private List<Bullet> aimedByBullets;

    private void Awake()
    {
        sprnd = transform.Find("Sprites/Body").GetComponent<SpriteRenderer>();
        shouldDestroyForce = false;

        aimedByBullets = new List<Bullet>(32);
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
        UpdateBullets();
        UpdateDead();
        UpdateDeadForce();
    }

    private void OnDestroy()
    {
        foreach (Bullet bullet in aimedByBullets)
        {
            bullet.OnLostEnemy(this);
        }
    }

    private void VibBody()
    {
        // Easing 기능-2 : Sin 함수의 진동을 활용하여 적 몸체의 디테일한 상하 이동을 구현했습니다.
        float y = Mathf.Sin(Time.time * bodySpeed) * bodyAmp + bodyBias;
        sprnd.transform.localPosition = new Vector3(0.0f, y, 0.0f);
    }

    private void UpdateBullets()
    {
        foreach (Bullet bullet in aimedByBullets)
        {
            bullet.OnTraceEnemy(this);
        }
    }

    private void UpdateDead()
    {
        if (health <= 0.0f)
        {
            GameManager.s_money += rewardMoney;

            Destroy(this.gameObject);
        }
    }

    private void UpdateDeadForce()
    {
        for (int i = 0; i < destroyPoints.Count; ++i)
        {
            Vector2 position = transform.position;
            int index = destroyPoints[i].destroyPointIndex;

            if (pathManager.IsArrived(position, index))
            {
                GameManager.s_curHeart -= 1;

                Destroy(this.gameObject);
                break;
            }
        }
    }

    public void Subscribe(Bullet bullet)
    {
        aimedByBullets.Add(bullet);
    }
}