using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform firePoint; // Vị trí xuất phát viên đạn
    public int bulletPrefabIndex = 0; // Chỉ số prefab viên đạn trong ObjectPool
    public string bulletTag = "Bullet";
    private bool isPaused = false;

    void Awake()
    {
        isPaused = false; // Reset trạng thái khi restart
    }
    void Update()
    {
        if (isPaused) return;
        if (Input.GetMouseButtonDown(0)) // Click chuột trái
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = ObjectPool.instance.GetPooledObject(bulletTag, firePoint.position, firePoint.rotation);
        if (bullet != null)
        {
            bullet.SetActive(true);
        }
    }

    public void SetPauseState(bool paused)
    {
        isPaused = paused;
    }
}
