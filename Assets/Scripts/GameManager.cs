using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool s_isGameStarted;
    public static int s_money;
    public static int s_maxHeart;
    public static int s_curHeart;
    public static float s_playtime;

    public static RectTransform s_ui_Lobby;
    public static RectTransform s_ui_Game;
    public static RectTransform s_ui_Result;

    public static float s_outOfBulletMinX = -20.0f;
    public static float s_outOfBulletMaxX = 20.0f;
    public static float s_outOfBulletMinY = -20.0f;
    public static float s_outOfBulletMaxY = 20.0f;

    public static Transform s_bulletContainer;
    public static Transform s_enemyContainer;

    public int maxHeart = 10;
    public int curHeart = 0;

    public int startMoney = 400;
    public float autoMoneyTime = 3.0f;
    public int autoMoney = 1;
    private float leftAutoMoneyTime;

    public PathManager pathManager;

    public RectTransform ui_Lobby;
    public RectTransform ui_Game;
    public RectTransform ui_Result;

    public Transform bulletContainer;
    public Transform enemyContainer;

    public List<SpawnPoint> spawnPoints;
    public List<DestroyPoint> destroyPoints;

    private Tile prevTile;
    private Tile nextTile;

    private void Awake()
    {
        s_ui_Lobby = ui_Lobby;
        s_ui_Game = ui_Game;
        s_ui_Result = ui_Result;

        s_bulletContainer = bulletContainer;
    }

    private void Start()
    {
        s_ui_Lobby.gameObject.SetActive(true);
        s_ui_Game.gameObject.SetActive(false);
        s_ui_Result.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (s_isGameStarted)
        {
            FindTile();
            SelectTile();
            GetAutoMoney();
            s_playtime += Time.deltaTime;
        }
    }

    public void OnClickButton_StartGame()
    {
        s_money = startMoney;

        curHeart = maxHeart;
        s_maxHeart = maxHeart;
        s_curHeart = curHeart;

        s_ui_Lobby.gameObject.SetActive(false);
        s_ui_Game.gameObject.SetActive(true);

        s_playtime = 0.0f;
        s_isGameStarted = true;
    }

    private void FindTile()
    {
        Vector3 posScreen = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(posScreen);
        RaycastHit hit;

        prevTile = nextTile;

        if (GameManager.s_isGameStarted && Physics.Raycast(ray, out hit))
            nextTile = hit.collider.transform.GetComponentInParent<Tile>();
        else
            nextTile = null;

        if (nextTile == prevTile)
        {
            // NOTE: Do Anything.
        }
        else if (nextTile == null && prevTile != null)
        {
            prevTile.isSelected = false;
        }
        else if (nextTile != null && prevTile == null)
        {
            nextTile.isSelected = nextTile.havingTower == null;
        }
        else
        {
            prevTile.isSelected = false;
            nextTile.isSelected = nextTile.havingTower == null;
        }
    }

    private void SelectTile()
    {
        if (!Input.GetMouseButtonDown(0) || nextTile == null)
            return;

        int nextLevel = (nextTile.havingTower?.level + 1) ?? 1;
        Tower boughtTower = Tower.BuyTowerOrNull(nextLevel);

        if (boughtTower == null)
            return;

        boughtTower.transform.SetParent(nextTile.transform);
        boughtTower.transform.localPosition = new Vector3(0.0f, 0.3f, -0.1f);

        if (nextTile.havingTower != null)
            Destroy(nextTile.havingTower.gameObject);

        nextTile.havingTower = boughtTower;
    }

    private void GetAutoMoney()
    {
        if (leftAutoMoneyTime > 0)
            leftAutoMoneyTime -= Time.deltaTime;
        else
        {
            leftAutoMoneyTime = autoMoneyTime;
            GameManager.s_money += autoMoney;
        }
    }

    private void GenerateEnemy()
    {
        for (int i = 0; i < spawnPoints.Count; ++i)
        {
            SpawnPoint point = spawnPoints[i];

            if (!point.shouldGenerate)
                continue;

            Enemy genEnemy = point.GenerateEnemyOrNull();

            genEnemy.pathManager = this.pathManager;
            genEnemy.pathTo = pathManager.GetNextIndex(point.spawnPointIndex);
            genEnemy.transform.parent = enemyContainer;

            Vector3 position = pathManager.GetPosition(point.spawnPointIndex);
            genEnemy.transform.position = position;
        }
    }
}
