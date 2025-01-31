using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Rigidbody2D rb;

    //variables about the player
    private Transform playerTransform;
    bool foundPlayer;

    [Header("speed")]
    public float EnemySpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        //StartCoroutine(Move());
    }


    // Update is called once per frame
    private void Update()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    bool checkPlayerPos()
    {
        //check position of player, if close start moving towards player
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        var distanceToPlayer = playerTransform.position.x - rb.position.x;
        if (distanceToPlayer >= 2) {foundPlayer = false;}
        else {foundPlayer = true;}
        return foundPlayer;
    }


    private IEnumerator Move()
    {
        while (true)
        {
            if (checkPlayerPos() == false)
            {
                //Move enemy randomly
                yield return new WaitForSeconds(1);
                Vector3 targetPos = RandomDir();
                yield return new WaitForSeconds(1);
                var moveTimer = .5f;
                while (moveTimer > 0)
                {
                    gameObject.transform.Translate(targetPos * Time.deltaTime * EnemySpeed);
                    moveTimer -= Time.deltaTime;
                    yield return null;
                }
            }
            else if (checkPlayerPos() == true)
            {
                //Move enemy towards player
                yield return new WaitForSeconds(1);
                var chaseTimer = Random.value;
                while (chaseTimer > 0)
                {
                    var targetPos = toPlayer();
                    gameObject.transform.Translate(targetPos * Time.deltaTime * EnemySpeed);
                    chaseTimer -= Time.deltaTime;
                    yield return null;
                }
            }
        }
    }
    Vector3 RandomDir()
    {
        var direction = Random.insideUnitSphere;
        return direction;
    }

    Vector3 toPlayer() { return playerTransform.position - transform.position;}

    }
