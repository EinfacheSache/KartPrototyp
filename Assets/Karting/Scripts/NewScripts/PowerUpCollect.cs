
using KartGame.KartSystems;
using System.Collections;
using TMPro;
using UnityEngine;


public class PowerUpCollect : MonoBehaviour
{

    public int powerUpCount;

    private float timer;
    private GameObject collectedPowerUp;
    private float rotateSpeed = 500;
    private float upDownSpeed = 2f;
    private float distanz = 0.25f;
    private float offset = 2f;

    [SerializeField] private int powerUpSpeedBoosted = 30;
    [SerializeField] private int powerUpSpeedDescred = 15;
    [SerializeField] private float powerUpSpeedTime = 2;

    [SerializeField] private float goodPowerUpLastPlayerMulti = 1.5f;
    [SerializeField] private float goodPowerUpFirstPlayerMulti = 0.25f;
    [SerializeField] private float goodPowerUpSamePlayerMulti = 1;

    [SerializeField] private GameObject otherPlayer;
    [SerializeField] private GameObject particel;
    [SerializeField] private GameObject powerUpParent;

    [SerializeField] private TextMeshProUGUI powerUpCountMesh;


    void Start()
    {
        powerUpCount = 0;
    }

    void Update()
    {
        CollectedPowerUpRotation();
    }

    private void FixedUpdate()
    {
        if (collectedPowerUp == null) {
            gameObject.GetComponent<ArcadeKart>().baseStats.TopSpeed = 20;
        }
    }


    private void OnTriggerEnter(Collider collider)
    {

        if (!powerUpParent.transform.Equals(collider.transform.parent))
        {
            return;
        }

        if (collectedPowerUp != null)
        {
            Destroy(collectedPowerUp);
        }

        powerUpCount++;
        powerUpCountMesh.text = powerUpCountMesh.text.Split(":")[0] + ": " + powerUpCount;

        collectedPowerUp = GameObject.CreatePrimitive(PrimitiveType.Cube);
        collectedPowerUp.GetComponent<BoxCollider>().isTrigger = true;
        collectedPowerUp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        collectedPowerUp.name = "collectedPowerUp";


        int progressSelfPlayer = gameObject.GetComponent<ResetController>().getCheckPointCount();
        int progressOtherPlayer = otherPlayer.GetComponent<ResetController>().getCheckPointCount();
        float multi;

        if(progressOtherPlayer == progressSelfPlayer) {
            multi = goodPowerUpSamePlayerMulti;
        }
        else if (progressOtherPlayer > progressSelfPlayer)
        {
            multi = goodPowerUpLastPlayerMulti;
        }else
            multi = goodPowerUpFirstPlayerMulti;
        

        if (Random.Range(0, 101) <= 100 * multi)
        {
            collectedPowerUp.GetComponent<Renderer>().material.color = Color.green;
            gameObject.GetComponent<ArcadeKart>().baseStats.TopSpeed = powerUpSpeedBoosted;
        }
        else
        {
            collectedPowerUp.GetComponent<Renderer>().material.color = Color.red;
            gameObject.GetComponent<ArcadeKart>().baseStats.TopSpeed = powerUpSpeedDescred;
        }

        GameObject vfx = (Instantiate(particel, gameObject.transform.position, Quaternion.identity));

        collider.GetComponent<MeshRenderer>().enabled = false;
        collider.GetComponent<CapsuleCollider>().enabled = false;

        Destroy(vfx, 1);
        Destroy(collectedPowerUp, powerUpSpeedTime);

        StartCoroutine(respawnTimer(collider));
    }



    private IEnumerator respawnTimer(Collider collider)
    {
        yield return new WaitForSeconds(8);

        collider.GetComponent<MeshRenderer>().enabled = true;
        collider.GetComponent<CapsuleCollider>().enabled = true;

    }

    void CollectedPowerUpRotation()
    {
        if(collectedPowerUp == null)
        {
            return;
        }

        timer += Time.deltaTime;
        float sin = Mathf.Sin(timer * upDownSpeed);

        collectedPowerUp.transform.eulerAngles += Vector3.up * rotateSpeed * Time.deltaTime;
        collectedPowerUp.transform.position = gameObject.transform.position + (Vector3.up * sin * distanz) + Vector3.up * offset;
    }
}
