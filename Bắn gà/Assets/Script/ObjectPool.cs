using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //public static ObjectPool instance;
    //[SerializeField] private int amountToPool = 5;

    //[SerializeField] private List<GameObject> prefabs;

    //private List<GameObject> pooledObjects = new List<GameObject>();

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //}

    //void Start()
    //{
    //    foreach (var prefab in prefabs)
    //    {
    //        for (int i = 0; i < amountToPool; i++)
    //        {
    //            GameObject obj = Instantiate(prefab);
    //            obj.SetActive(false);
    //            pooledObjects.Add(obj);
    //        }
    //    }
    //}

    ////void Start()
    ////{
    ////    if (prefabs.Count == 0) return;

    ////    int objectsPerPrefab = amountToPool / prefabs.Count; // Số lượng tối đa mỗi loại prefab
    ////    int remainder = amountToPool % prefabs.Count; // Phần dư (nếu không chia hết)

    ////    for (int i = 0; i < prefabs.Count; i++)
    ////    {
    ////        int count = objectsPerPrefab + (i < remainder ? 1 : 0); // Nếu có phần dư, một số prefab sẽ nhận thêm 1 object

    ////        for (int j = 0; j < count; j++)
    ////        {
    ////            GameObject obj = Instantiate(prefabs[i]);
    ////            obj.SetActive(false);
    ////            pooledObjects.Add(obj);
    ////        }
    ////    }
    ////}

    //public GameObject GetPooledObject(Vector3 startPosition, Quaternion rotation, int prefabIndex)
    //{
    //    if (prefabIndex < 0 || prefabIndex >= prefabs.Count)
    //    {
    //        return null;
    //    }

    //    foreach (var obj in pooledObjects)
    //    {
    //        if (!obj.activeInHierarchy && obj.CompareTag(prefabs[prefabIndex].tag))
    //        {
    //            obj.transform.position = startPosition;
    //            obj.transform.rotation = rotation;
    //            obj.SetActive(true);
    //            return obj;
    //        }
    //    }
    //    return null;
    //}

    //void Update()
    //{

    //}

    public static ObjectPool instance;

    [SerializeField] private int amountToPool = 5;
    [SerializeField] private List<GameObject> prefabs;

    private Dictionary<string, List<GameObject>> poolDictionary = new Dictionary<string, List<GameObject>>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        foreach (var prefab in prefabs)
        {
            string tag = prefab.tag;
            poolDictionary[tag] = new List<GameObject>();

            for (int i = 0; i < amountToPool; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                poolDictionary[tag].Add(obj);
            }
        }
    }

    public GameObject GetPooledObject(string tag, Vector3 startPosition, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"No objects of tag {tag} exist in the pool!");
            return null;
        }

        foreach (var obj in poolDictionary[tag])
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = startPosition;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }
        }

        return null; // Hết đối tượng trong pool
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }

}
