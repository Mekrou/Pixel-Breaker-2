using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour
{
    [Header("Paddle Object")]
    [SerializeField] PaddleController paddle;
    [Header("Diamond Tuning")]
    [SerializeField] float maxVelocity;
    [Tooltip("The velocity given to the diamond upon launch")]
    [SerializeField] float launchVelocityY;
    [SerializeField] float launchVelocityX;

    [Header("Sound Effects")]
    [SerializeField] AudioClip[] audioClips;

    private float offsetY;
    private Vector2 paddlePos;
    private Vector2 diamondPos;

    Rigidbody2D rb;
    
    private bool hasLaunched = false;
    private bool debounce = false;

    private void Start()
    {
        diamondPos = gameObject.transform.position;
        offsetY = (diamondPos.y - paddle.transform.position.y);

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ClampVelocityOfDiamond();
        if (!hasLaunched)
        {
            AttatchDiamondToPaddle();
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            hasLaunched = true;
            LaunchDiamond();
        }
    }

    private void AttatchDiamondToPaddle()
    {
        paddlePos = paddle.transform.position;
        gameObject.transform.position = new Vector2(paddlePos.x, paddlePos.y + offsetY);
    }

    private void LaunchDiamond()
    {
        if (!debounce)
        {
            rb.velocity = new Vector2(launchVelocityX, launchVelocityY);
            debounce = true;
        }
    }

    private void ClampVelocityOfDiamond()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 diamondVelocityTweak = new Vector2(Random.Range(-2.5f, 5f), Random.Range(-2.5f, 5f));
        
        rb.velocity += diamondVelocityTweak;
        
        if (collision.gameObject.GetComponent<Block>())
        {
            AudioClip randomClip = audioClips[Random.Range(0, audioClips.Length - 1)];

            AudioSource.PlayClipAtPoint(randomClip, Camera.main.transform.position, 0.5f);
        }
    }
}
