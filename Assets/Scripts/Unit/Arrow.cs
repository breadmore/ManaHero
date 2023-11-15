using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Vector2 enemyPos;
    public float speed = 10f;
    public float damage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        LivingEntity target = collision.GetComponent<LivingEntity>();

        if (collision.CompareTag("Monster"))
        {
            if (target != null)
            {
                Debug.Log("Living");
                target.OnDamage(damage);
                Destroy(gameObject);
            }
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        Turn(enemyPos);
        Vector2 direction = enemyPos - new Vector2(transform.position.x, transform.position.y);
        float distanceArrowFrame = speed * Time.deltaTime;
        transform.Translate(direction.normalized * distanceArrowFrame, Space.World);

    }

    public void Turn(Vector2 pos)
    {

        float x = Mathf.Abs(transform.localScale.x);
        if (transform.position.x > pos.x)
            transform.localScale = new Vector3(x, x, x);
        if (transform.position.x < pos.x)
            transform.localScale = new Vector3(-x, x, x);
    }
}
