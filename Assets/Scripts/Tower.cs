using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int level;
    public float attackFrequency;
    public float bulletSpeed;
    public float damage;
    public int price;

    public float towerBallAmp = 0.02f;
    public float towerBallBias = 0.01f;
    public float towerBallSpeed = 3.0f;

    private float leftAttackTime;

    private Transform towerBall;
    private Vector3 towerBallLocalPositionOffset;

    public static Tower BuyTowerOrNull(int level)
    {
        Debug.Assert(level > 0, "param 'level' should be positive number.");

        try
        {
            GameObject obj_tower = Resources.Load(string.Format("Tower/{0}Lv", level)) as GameObject;
            Tower src_tower = obj_tower.GetComponent<Tower>();

            if (GameManager.s_money < src_tower.price)
                return null;
            else
            {
                GameManager.s_money -= src_tower.price;
                return GameObject.Instantiate<Tower>(src_tower);
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private void Awake()
    {
        leftAttackTime = 1.0f / attackFrequency;
        towerBall = transform.Find("TowerBall");
        towerBallLocalPositionOffset = towerBall.localPosition;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        leftAttackTime -= Time.deltaTime;

        if (leftAttackTime <= 0.0f)
        {
            ShootBullet();

            while (leftAttackTime <= 0.0f)
                leftAttackTime += (1.0f / attackFrequency);
        }

        VibTowerBall();
    }

    private void VibTowerBall()
    {
        float y = Mathf.Sin(Time.time * towerBallSpeed) * towerBallAmp + towerBallBias;
        towerBall.localPosition = towerBallLocalPositionOffset + new Vector3(0.0f, y, 0.0f);
    }

    private void ShootBullet()
    {
        Vector3 direction;
        bool canShoot = TryGetBulletDirection(out direction);

        if (!canShoot)
            return;

        GameObject obj_bullet = Resources.Load("Bullet") as GameObject;
        Bullet bullet = GameObject.Instantiate(obj_bullet).GetComponent<Bullet>();

        bullet.transform.parent = GameManager.s_bulletContainer;

        Vector3 dPosition = new Vector3(0.0f, 0.3f, -0.02f);
        Vector3 position = transform.position + dPosition;
        position.z = -1.0f;
        bullet.transform.position = position;

        bullet.direction = direction;
        bullet.speed = this.bulletSpeed;
        bullet.damage = this.damage;
    }

    private bool TryGetBulletDirection(out Vector3 direction)
    {
        Transform enemies = GameManager.s_enemyContainer;

        Vector3 dir = Vector3.zero;
        float distance = float.MaxValue;
        int index = -1;

        for (int i = 0; i <  enemies.childCount; ++i)
        {
            Transform enemy = enemies.GetChild(i);

            Vector2 dp = enemy.position - transform.position;

            float dist = dp.x * dp.x + dp.y * dp.y;

            if (dist < distance)
            {
                index = i;
                distance = dist;
                dir = dp.normalized;
            }
        }

        direction = dir;

        return index != -1;
    }
}
