using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 inputDirection;
    GameObject currentGun;
    Rigidbody2D currentRB;
    [Header("Prefabs")]
    public GameObject startingGun;
    Gun gunScript;

    [Header("Movement")]
    public float moveSpeed;

    [Header("Health")]
    public float playerHealth;

    CircleCollider2D switchCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switchCollider = GetComponent<CircleCollider2D>(); startingGun = Instantiate(startingGun);
        startingGun.transform.position = Vector3.zero;
        UpdateCurrentGun(startingGun);
    }

    // Update is called once per frame
    void Update()
    {
        if(!gunScript.hasKnockback) 
            currentRB.MovePosition(currentRB.position + 
                inputDirection * moveSpeed * Time.deltaTime);
        transform.position = currentRB.position;

        if (playerHealth <= 0)
        {
            //player is dead
            playerHealth = 0;
            inputDirection = Vector3.zero;
            StartCoroutine(Revive());
        }
        RotateGun();
    }
    void UpdateCurrentGun(GameObject newGun)
    {
        currentGun = newGun;
        Destroy(currentGun.GetComponent<BoxCollider2D>());
        currentRB = currentGun.GetComponent<Rigidbody2D>();
        currentGun.tag = "Player";
        gunScript = currentGun.GetComponent<Gun>();
    }

    void RotateGun()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 direction = (mousePosition - currentGun.transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 180f;
        currentGun.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    //Move the gun with WASD or Arrow Keys
    public void OnMove(InputAction.CallbackContext context) =>
        inputDirection = context.ReadValue<Vector2>();
    //Switch gun with Space
    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, switchCollider.radius);
            foreach (Collider2D collider in colliders)
                if (collider.CompareTag("Gun") && collider != currentGun)
                {
                    Destroy(currentGun);
                    UpdateCurrentGun(collider.gameObject);
                    break;
                }
        }
    }

    private IEnumerator Revive()
    {
        yield return new WaitForSeconds(3);
        playerHealth = 50;
    }
    //Shoot the gun with LMB
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gunScript.ShootBullet();
        }
    }
   

}
