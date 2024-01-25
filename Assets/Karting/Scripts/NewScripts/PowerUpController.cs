
using System.Collections;
using UnityEngine;

public class PowerUpController : MonoBehaviour    
{
    private float timer;
    private Vector3 powerUpPos;

    [SerializeField] private float distanz = 0.225f;
    [SerializeField] private float rotateSpeed = 300;
    [SerializeField] private float upDownSpeed = 1f;
    [SerializeField] private float offset = 0.5f;


    private void Start()
    {
        powerUpPos = transform.position;
    }

    void Update()
    {

        timer += Time.deltaTime;
        float sin = Mathf.Sin(timer * upDownSpeed);

        transform.eulerAngles += Vector3.up*rotateSpeed*Time.deltaTime;
        transform.position = powerUpPos + (Vector3.up * sin * distanz) + Vector3.up * offset;
    }

    private void OnTriggerEnter(Collider collider)
    {
        StartCoroutine(respawnTimer(collider));
    }

    private IEnumerator respawnTimer(Collider collider)
    {
        yield return new WaitForSeconds(8);

        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }
}
