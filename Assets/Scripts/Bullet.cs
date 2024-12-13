using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public float damage;
    [HideInInspector] public Vector3 targetPosition;

    private Vector3 distanceVector;
    private bool attacked;
    private bool targetLost;

    public void OnTraceEnemy(Enemy enemy)
    {
        targetPosition = enemy.transform.position;

        if (!attacked && GameManager.IsNearByTwoPosition(targetPosition, transform.position))
        {
            enemy.health -= damage;
            attacked = true;
        }
    }

    public void OnLostEnemy(Enemy enemy)
    {
        targetLost = true;
    }

    private void Awake()
    {
        attacked = false;
        targetLost = false;
    }

    private void FixedUpdate()
    {
        distanceVector = targetPosition - transform.position;
        transform.position += distanceVector.normalized * speed * Time.fixedDeltaTime;
    }

    private void Update()
    {
        if (attacked || (targetLost && GameManager.IsNearByTwoPosition(targetPosition, transform.position)))
        {
            Destroy(this.gameObject);
        }
    }

    
}