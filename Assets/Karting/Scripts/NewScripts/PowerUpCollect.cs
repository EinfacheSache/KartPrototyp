
using KartGame.KartSystems;
using System.Collections;
using TMPro;
using UnityEngine;


public class PowerUpCollect : MonoBehaviour
{


    public int powerUpCount;


    private GameObject shootedPowerUp;
    private bool powerUpCooldown;
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
        ShotedPowerUp();
    }

    private void FixedUpdate()
    {
        if (collectedPowerUp == null && gameObject.GetComponent<ArcadeKart>().baseStats.TopSpeed != 5) {
            gameObject.GetComponent<ArcadeKart>().baseStats.TopSpeed = 20;
        }
    }


    private void OnTriggerEnter(Collider collider)
    {

        if (!powerUpParent.transform.Equals(collider.transform.parent))
        {
            return;
        }

        if (powerUpCooldown)
        {
            return;
        }

        powerUpCooldown = true;

        if (collectedPowerUp != null && collectedPowerUp.GetComponent<Renderer>().material.color != Color.yellow)
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

        float random = (Random.Range(0, 100));

        if(random < 5 && shootedPowerUp == null) {
            collectedPowerUp.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (random <= 100 * multi)
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

        StartCoroutine(PowerUpCooldown());

        Destroy(vfx, 1);

        if(collectedPowerUp.GetComponent<Renderer>().material.color == Color.yellow)
        {
            StartCoroutine(ShotTimer(collectedPowerUp));
            return;
        }

        Destroy(collectedPowerUp, powerUpSpeedTime);
       
    }

    float hitTime = 0;

    void CollectedPowerUpRotation()
    {
        if(collectedPowerUp == null)
        {
            return;
        }

        if (collectedPowerUp.GetComponent<Renderer>().material.color == Color.yellow && shootedPowerUp != null)
        {
            return;
        }

        timer += Time.deltaTime;
        float sin = Mathf.Sin(timer * upDownSpeed);

        collectedPowerUp.transform.eulerAngles += Vector3.up * rotateSpeed * Time.deltaTime;
        collectedPowerUp.transform.position = gameObject.transform.position + (Vector3.up * sin * distanz) + Vector3.up * offset;
    }



    void ShotedPowerUp()
    {

        if (shootedPowerUp != null)
        {

            hitTime += Time.deltaTime;
            shootedPowerUp.transform.position = Vector3.Lerp(shootedPowerUp.transform.position, otherPlayer.transform.position, hitTime / 15);
        }
    }


    private IEnumerator ShotTimer(GameObject powerUp)
    {
        yield return new WaitForSeconds(2);

        shootedPowerUp = powerUp;
        shootedPowerUp.layer = 15;
    }

    private IEnumerator PowerUpCooldown()
    {
        yield return new WaitForSeconds(2);

        powerUpCooldown = false;
    }
}
