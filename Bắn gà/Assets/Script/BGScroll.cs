using UnityEngine;

public class BGScroll : MonoBehaviour
{
    //public float speed = 4f;
    //private float bgHeight;

    //void Start()
    //{
    //    // Lấy chiều cao của background dựa vào SpriteRenderer
    //    bgHeight = GetComponent<SpriteRenderer>().bounds.size.y;
    //}

    //void Update()
    //{
    //    // Di chuyển background xuống
    //    transform.Translate(Vector3.down * speed * Time.deltaTime);

    //    // Nếu background ra khỏi màn hình, đặt lại ngay phía trên
    //    if (transform.position.y < -bgHeight)
    //    {
    //        RepositionBackground();
    //    }
    //}

    //void RepositionBackground()
    //{
    //    // Tìm background cao nhất để đặt background hiện tại lên trên cùng
    //    GameObject highestBG = FindHighestBackground();
    //    transform.position = new Vector3(transform.position.x, highestBG.transform.position.y + bgHeight, transform.position.z);
    //}

    //GameObject FindHighestBackground()
    //{
    //    GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("Background");
    //    GameObject highestBG = backgrounds[0];

    //    foreach (GameObject bg in backgrounds)
    //    {
    //        if (bg.transform.position.y > highestBG.transform.position.y)
    //        {
    //            highestBG = bg;
    //        }
    //    }

    //    return highestBG;
    //}

    public float speed = 4f;
    public float overlapOffset = 0.2f; // Điều chỉnh mức độ chồng lấn

    void Update()
    {
        // Di chuyển background xuống
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Kiểm tra nếu background ra khỏi màn hình thì reposition
        float lowestPoint = Camera.main.transform.position.y - Camera.main.orthographicSize - GetComponent<SpriteRenderer>().bounds.size.y;
        if (transform.position.y < lowestPoint)
        {
            RepositionBackground();
        }
    }

    void RepositionBackground()
    {
        GameObject highestBG = FindHighestBackground();

        // Lấy chiều cao thực tế của background hiện tại
        float bgHeight = GetComponent<SpriteRenderer>().bounds.size.y;

        // Lấy chiều cao thực tế của background cao nhất
        float highestBGHeight = highestBG.GetComponent<SpriteRenderer>().bounds.size.y;

        // Cập nhật vị trí background hiện tại lên trên background cao nhất + overlapOffset
        transform.position = new Vector3(
            transform.position.x,
            highestBG.transform.position.y + (highestBGHeight / 2) + (bgHeight / 2) - overlapOffset,
            transform.position.z
        );
    }

    GameObject FindHighestBackground()
    {
        GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("Background");
        GameObject highestBG = backgrounds[0];

        foreach (GameObject bg in backgrounds)
        {
            if (bg.transform.position.y > highestBG.transform.position.y)
            {
                highestBG = bg;
            }
        }

        return highestBG;
    }
}
