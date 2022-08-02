using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Movement : MonoBehaviour
{
    public float speed = 2f;
    Camera cam;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    [SerializeField] public float horizontal = 8.5f;
    
    [SerializeField] public float vertical = 4f;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        //Rotate the player
        PlayerRotation();
        //PlayerMovement();
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        float horizontalOffset = horizontalMove * speed * Time.deltaTime; 
        float verticalOffset = verticalMove * speed * Time.deltaTime;

        float horizontalPosition = transform.position.x + horizontalOffset;
        float clampedHorizPos = Mathf.Clamp(horizontalPosition, -horizontal, horizontal);
        
        float verticalPosition = transform.position.y + verticalOffset;
        float clampedVerticPos = Mathf.Clamp(verticalPosition, -vertical, vertical);

        transform.position = new Vector3(clampedHorizPos, clampedVerticPos, transform.position.z);

    }
    void PlayerRotation()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = cam.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y);
        transform.up = direction;
    }
    // void PlayerMovement()
    // {
    //     //Move any direction using WASD
    //     if (Input.GetKey("w") == true)
    //     {
    //         transform.position += Vector3.up * speed * Time.deltaTime;
    //     }
    //     if (Input.GetKey("s") == true)
    //     {
    //         transform.position += Vector3.down * speed * Time.deltaTime;
    //     }
    //     if (Input.GetKey("a") == true)
    //     {
    //         transform.position += Vector3.left * speed * Time.deltaTime;
    //     }
    //     if (Input.GetKey("d") == true)
    //     {
    //         transform.position += Vector3.right * speed * Time.deltaTime;
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;

           this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDied();
        }
    }

}
