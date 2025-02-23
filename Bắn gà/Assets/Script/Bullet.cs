using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 24f;
    [SerializeField] private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.right * speed;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            ObjectPool.instance.ReturnToPool(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.Die(); // Gọi hàm xử lý chết của Enemy
            }

            // Đưa đạn trở về Object Pool
            ObjectPool.instance.ReturnToPool(gameObject);
        }
    }

}
