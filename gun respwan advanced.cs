using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public List<Gun> guns;
    public int currentGunIndex = 0;
    public Text inventoryText;

    void Start()
    {
        SetActiveGun(currentGunIndex);
        UpdateInventoryText();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            guns[currentGunIndex].FireBullet();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            CycleGuns();
        }
    }

    void CycleGuns()
    {
        currentGunIndex++;
        if (currentGunIndex >= guns.Count)
        {
            currentGunIndex = 0;
        }
        SetActiveGun(currentGunIndex);
    }

    void SetActiveGun(int index)
    {
        for (int i = 0; i < guns.Count; i++)
        {
            if (i == index)
            {
                guns[i].gameObject.SetActive(true);
            }
            else
            {
                guns[i].gameObject.SetActive(false);
            }
        }
    }

    public List<Item> inventory = new List<Item>();

    void UpdateInventoryText()
    {
        inventoryText.text = "Inventory:\n";
        foreach (Item item in inventory)
        {
            inventoryText.text += "- " + item.name + "\n";
        }
    }

    public void AddToInventory(Item item)
    {
        inventory.Add(item);
        UpdateInventoryText();
    }

    public void RemoveFromInventory(Item item)
    {
        inventory.Remove(item);
        UpdateInventoryText();
    }
}

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

    public void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Destroy(bullet, 3.0f);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit,

100.0f))
{
if (hit.collider.CompareTag("Enemy"))
{
Enemy enemy = hit.collider.GetComponent<Enemy>();
enemy.TakeDamage(10);
}
else if (hit.collider.CompareTag("Item"))
{
Item item = hit.collider.GetComponent<Item>();
item.Pickup();
}
}
}
}

public class Item : MonoBehaviour
{
public string name;


public void Pickup()
{
    PlayerController playerController = FindObjectOfType<PlayerController>();
    if (playerController != null)
    {
        playerController.AddToInventory(this);
        Destroy(gameObject);
    }
}

}

public class Enemy : MonoBehaviour
{
public int health = 100;
public Text oopsText;


public void TakeDamage(int damage)
{
    health -= damage;
    if (health <= 0)
    {
        Die();
    }
}

void Die()
{
    Destroy(gameObject);
}

private void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        oopsText.text = "Oops, you got caught!";
        oopsText.gameObject.SetActive(true);
    }
}

}
