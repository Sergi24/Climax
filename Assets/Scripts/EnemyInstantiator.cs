using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiator : MonoBehaviour
{
    public GameObject enemy;
    public float rate, circularSpeed, radius;

    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        angle = 0;
        StartCoroutine(InstantiateEnemy());
    }

    public void Update()
    {
        transform.Translate(Vector3.right * circularSpeed * Mathf.Cos(angle * Mathf.Deg2Rad) * Time.deltaTime);
        transform.Translate(Vector3.forward * circularSpeed * Mathf.Sin(angle * Mathf.Deg2Rad) * Time.deltaTime);
        angle += radius;
        if (angle >= 360)
        {
            angle = 360 - angle;
        }
    }

    private IEnumerator InstantiateEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(rate);
            Instantiate(enemy, transform.position, transform.rotation);
        }
    }
}
