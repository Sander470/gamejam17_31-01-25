using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public int health;
    public int knockback;
    public int damage;
    public int speed;
    public int reload;
    public bool hasKnockback = false;
    float timer;

    public GameObject bulletPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = reload;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0 ) 
            timer -= Time.deltaTime;
    }

    public void ShootBullet()
    {
        if (timer > 0)
            return;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 shootDirection = (mousePosition - transform.position).normalized;
        bullet.GetComponent<Bullet>().bulletSpeed = speed;
        bullet.GetComponent<Bullet>().shootDirection = shootDirection;

        StartCoroutine(KnockbackMover(shootDirection));
        timer = reload;
    }
    IEnumerator KnockbackMover(Vector2 bulletDirection)
    {
        float elapsedTime = 0f;
        float time = 0.5f;
        hasKnockback = true;        
        while (elapsedTime < time)
        {
            transform.Translate(-bulletDirection * knockback * Time.deltaTime);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        hasKnockback = false;
    }


}
