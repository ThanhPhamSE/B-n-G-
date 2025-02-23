using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public Transform pivot; // Điểm xoay của súng
    public float maxAngle = 75f; // Giới hạn mỗi bên 75 độ
    private float lastValidAngle = 90f; // Lưu góc hợp lệ gần nhất
    private bool isPaused = false;


    void Awake()
    {
        isPaused = false; // Reset trạng thái khi restart
    }
    void Start()
    {
        pivot.rotation = Quaternion.Euler(0, 0, 90);
    }

    void Update()
    {
        if (isPaused) return;
        // Lấy vị trí chuột trong thế giới
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // Tính hướng từ pivot đến chuột
        Vector3 direction = mousePos - pivot.position;

        // Tính góc giữa hướng đó và trục X
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Chuyển góc về hệ quy chiếu của súng (so với góc 90° ban đầu)
        float relativeAngle = angle - 90f;

        // Kiểm tra xem góc có nằm trong phạm vi [-75°, 75°] không
        if (relativeAngle >= -maxAngle && relativeAngle <= maxAngle)
        {
            lastValidAngle = relativeAngle; // Lưu lại góc hợp lệ cuối cùng
        }

        // Cộng lại 90° để có góc thực tế
        float finalAngle = lastValidAngle + 90f;

        // Xoay súng
        pivot.rotation = Quaternion.Euler(0, 0, finalAngle);
    }

    public void SetPauseState(bool paused)
    {
        isPaused = paused;
    }
}
