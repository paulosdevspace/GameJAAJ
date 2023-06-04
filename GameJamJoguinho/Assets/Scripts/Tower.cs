using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Enemy targetEnemy;
    private float lookForTargetTimer;
    private float lookForTargetTimerMax = .2f;
    private Vector3 projectileSpawnPosition;

    [SerializeField] private float shooterTimerMax;
    private float shooterTimer;

    private void Awake()
    {
        projectileSpawnPosition = transform.Find("projectileSpawnPosition").position;
    }
    private void Update()
    {
        HandleTargeting();
        HandleShooting();
    }
    private void HandleTargeting()
    {
        lookForTargetTimer -= Time.deltaTime;
        if (lookForTargetTimer < 0f)
        {
            lookForTargetTimer += lookForTargetTimerMax;
            LookForTarget();
        }
    }
    private void HandleShooting()
    {
        shooterTimer -= Time.deltaTime;
        if (shooterTimer <= 0f)
        {
            shooterTimer += shooterTimerMax;

            if (targetEnemy != null)
            {
                Projectile.Create(projectileSpawnPosition, targetEnemy);
            }
        }
    }

    private void LookForTarget()
    {
        float targetMaxRadius = 28f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();

            if (enemy != null)
            {
                if (targetEnemy == null)
                {
                    targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        targetEnemy = enemy;
                    }
                }

            }
        }

    }
}
