using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 inputDirection;
    GameObject currentGun;
    Rigidbody2D currentRB;
    [Header("Starting Gun")]
    public GameObject startingGun;

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
    }
    void UpdateCurrentGun(GameObject newGun)
    {
        currentGun = newGun;
        Destroy(currentGun.GetComponent<BoxCollider2D>());
        currentRB = currentGun.GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context) =>
        inputDirection = context.ReadValue<Vector2>();
    
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
}
