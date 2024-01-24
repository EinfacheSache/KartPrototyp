

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class IDGenerator : MonoBehaviour
{

    [SerializeField] private GameObject firstCheckPoint;
    [SerializeField] private GameObject[] checkPointArray;
    [SerializeField] private GameObject lastCheckPoint;

    int idCount = 1;

    private void OnValidate()
    {
        List<GameObject> checkPointList = checkPointArray.ToList();

        firstCheckPoint.GetComponent<CheckpointTrigger>().id = 0;
        lastCheckPoint.GetComponent<CheckpointTrigger>().id = checkPointList.Count - 1;

        startCalculation(checkPointList, firstCheckPoint);

        idCount = 1;
    }


    void startCalculation(List<GameObject> list, GameObject last)
    {
        float minDistance = float.MaxValue;
        GameObject nearestCheckPoint = null;


        list.Remove(last);


        foreach (var item2 in list)
        {
            if (item2 == last || item2 == lastCheckPoint)
            {
                continue;
            }

            float distance = Vector3.Distance(last.transform.position, item2.transform.position);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestCheckPoint = item2;
            }
        }

        if (nearestCheckPoint != null)
        {
            nearestCheckPoint.GetComponent<CheckpointTrigger>().id = idCount;
            idCount++;
            startCalculation(list, nearestCheckPoint);
        }
    }
}

