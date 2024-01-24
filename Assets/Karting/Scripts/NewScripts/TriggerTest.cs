using UnityEngine;

public class TriggerTest : MonoBehaviour
{


    public Transform playerPos;
    public GameObject particel;

    private bool isTriggert;
    private GameObject cube;
    
    // Update is called once per frame
    void Update()
    {
        if (isTriggert)
        {
            cube.transform.position = new Vector3(playerPos.position.x, playerPos.position.y + 2, playerPos.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggert)
        {
            cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.GetComponent<Renderer>().material.color = Color.red;
            cube.name = "Self";


            var vfx = Instantiate(particel, playerPos.position, Quaternion.identity);
            Destroy(vfx, 5);

            isTriggert = true;
        }
        else
        {
            isTriggert = false;
            Destroy(cube);
        }   
    }
}
