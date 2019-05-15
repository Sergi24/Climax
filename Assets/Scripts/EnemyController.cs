using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;

    private float timeCount;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance.gameObject;
        timeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.transform.position - transform.position), timeCount);
        timeCount += Time.deltaTime;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("PlayerAttack"))
        {
            Destroy(gameObject);
        }
    }
}
