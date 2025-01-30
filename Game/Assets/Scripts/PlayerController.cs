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
    }
    void UpdateCurrentGun(GameObject newGun)
    {
        currentGun = newGun;
        Destroy(currentGun.GetComponent<BoxCollider2D>());
        currentRB = currentGun.GetComponent<Rigidbody2D>();
        currentGun.tag = "Player";
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
}
