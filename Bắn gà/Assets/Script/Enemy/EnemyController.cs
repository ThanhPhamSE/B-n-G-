using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform[] wayPoints;

    //[SerializeField] private Transform firePoint;
    //[SerializeField] private int bulletPrefabIndex = 0;
    //[SerializeField] private float shootInterval = 10f;


    private int currentWayPointIndex;
    private bool active;
    void Start()
    {
        // Kích hoạt bắn sau shootInterval giây, và lặp lại mỗi shootInterval giây
        //InvokeRepeating(nameof(Shoot), shootInterval, shootInterval);
    }


    void Update()
    {
        if (!active)
        {
            return;
        }
        int nextWayPoint = currentWayPointIndex + 1;
        if (nextWayPoint > wayPoints.Length - 1)
        {
            nextWayPoint = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[nextWayPoint].position, moveSpeed * Time.deltaTime);
        if (transform.position == wayPoints[nextWayPoint].position)
        {
            currentWayPointIndex = nextWayPoint;
        }
    }

    public void Init(Transform[] IWayPoints)
    {
        wayPoints = IWayPoints;
        active = true;
        transform.position = wayPoints[0].position;
    }

    public void Die()
    {
        GameManager.instance.EnemyKilled();
        gameObject.SetActive(false); // Tắt enemy
        // Nếu bạn dùng Object Pool thì thay bằng ObjectPool.instance.ReturnToPool(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
