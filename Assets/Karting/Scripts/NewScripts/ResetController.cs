using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetController : MonoBehaviour
{

    [SerializeField] private GameObject secondLastCheckpoint;
    [SerializeField] private GameObject lastCheckpoint;
    [SerializeField] private GameObject otherPlayer;
    [SerializeField] private int checkPointCount;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer.ToString().Equals("7"))
        {


            if (collider.gameObject == lastCheckpoint)
            {
                return;
            }

            if (collider.gameObject == secondLastCheckpoint)
            {
                Respawn();
                return;
            }

            checkPointCount++;
            secondLastCheckpoint = lastCheckpoint;
            lastCheckpoint = collider.gameObject;
        }

        if (collider.gameObject.layer.ToString().Equals("0") || collider.gameObject.layer.ToString().Equals("9"))
        {
            Respawn();
        }
    }

    void Respawn()
    {
        transform.position = lastCheckpoint.transform.position;
        transform.eulerAngles = lastCheckpoint.transform.eulerAngles;
        transform.GetComponent<Rigidbody>().velocity = new Vector3();
    }

    public int getCheckPointCount()
    {
        return checkPointCount;
    }
}
