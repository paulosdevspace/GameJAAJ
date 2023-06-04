using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaHead : MonoBehaviour
{
    /*private Enemy targetEnemy;

    private void Update()
    {
        /*Vector3 rotation = transform.position - enemy.transform.position;*/
    /*float targetMaxRadius = 28f;
    Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);
    foreach (Collider2D collider2D in collider2DArray)
    {

        Enemy enemy = collider2D.GetComponent<Enemy>();

        Vector3 rotation = transform.position - enemy.transform.position;
        Debug.Log("lol");
    }
    Vector3 rotation = transform.position;
    float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

    if (Input.GetKeyDown(KeyCode.F))
    {
        Debug.Log("lol");
    }
}*/
    /*void Update()
    {
        Enemy enemy = GetComponent<Enemy>();
        Vector3 rotation = transform.position - targetEnemy.transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
    }*/

    private Enemy targetEnemy;
    private Vector3 lastMoveDir;


    private void Update()
    {
        Vector3 rotateDir;
        if (targetEnemy != null)
        {
            rotateDir = (targetEnemy.transform.position - transform.position);
            lastMoveDir = rotateDir;
        }
        else
        {
            rotateDir = lastMoveDir;

        }

        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(rotateDir));

        
    }
}
