using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Text oopsText;

    private float fireRate = 0.1f;
    private float nextFire = 0.0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            FireBullet();
        }
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Destroy(bullet, 3.0f);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.transform.tag == "Player" || hit.transform.tag == "Enemy")
            {
                hit.transform.gameObject.SetActive(false);
                Invoke("Respawn", 5.0f);
                oopsText.gameObject.SetActive(true);
                Invoke("HideText", 2.0f);
            }
        }
    }

    void Respawn()
    {
        GameObject[] respawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        int randomRespawn = Random.Range(0, respawnPoints.Length);
        transform.position = respawnPoints[randomRespawn].transform.position;
        gameObject.SetActive(true);
    }

    void HideText()
    {
        oopsText.gameObject.SetActive(false);
    }
}
