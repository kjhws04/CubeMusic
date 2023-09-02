using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectInfo
{
    public GameObject goPrefabs;
    public int count;
    public Transform tfPoolParent;
}

public class PoolManager : MonoBehaviour
{
    [SerializeField] ObjectInfo[] objectInfo = null;

    public static PoolManager _instance;

    public Queue<GameObject> noteQueue = new Queue<GameObject>();

    private void Start()
    {
        _instance = this;
        noteQueue = InsertQueue(objectInfo[0]); //첫번째 배열

        //ex = InsertQueue(objectInfo[1]); //풀링을 사용할 자원 추가용
    }

    Queue<GameObject> InsertQueue(ObjectInfo p_objectInfo)
    {
        Queue<GameObject> t_queue = new Queue<GameObject>();
        for (int i = 0; i < p_objectInfo.count; i++)
        {
            GameObject t_clone = Instantiate(p_objectInfo.goPrefabs, transform.position, Quaternion.identity);
            t_clone.SetActive(false);
            if (p_objectInfo.tfPoolParent != null)
                t_clone.transform.SetParent(p_objectInfo.tfPoolParent);
            else
                t_clone.transform.SetParent(this.transform);

            t_queue.Enqueue(t_clone);
        }

        return t_queue;
    }
}
