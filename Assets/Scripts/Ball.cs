using System;
using UnityEngine;



public class Ball : MonoBehaviour
{

    public const float DIRECTIONAL_KICK_FORCE = 1f;
    public const float UPWARDS_KICK_FORCE = 4f;
    public const float SPIN_FORCE = 0.5f;
    private Rigidbody rb;

    private GameObject left_wall;
    private GameObject right_wall;

    public Vector3 mouse_on_screen;
    public Vector3 mouse_world_position;
    public Vector3 original_ball_position;

    public GameManager game_manager;
    public UIBuilder ui;
    public int score = 0;
    public int high_score = 0;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera camera = Camera.main;
        rb = GetComponent<Rigidbody>();
        original_ball_position = rb.transform.position;
        ui = GetComponent<UIBuilder>();
        game_manager = GetComponent<GameManager>();

        left_wall = GameObject.Find("LeftWall");
        right_wall = GameObject.Find("RightWall");

        float distance = Vector3.Distance(Camera.main.transform.position, transform.position);

        Vector3 left_position = camera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 right_position = camera.ViewportToWorldPoint(new Vector3(1, 0, distance));

        left_wall.transform.position = left_position;
        right_wall.transform.position = right_position;
    }

    // Update is called once per frame
    void Update()
    {

        //reset ball position and movement when "R" is pressed on keyboard
        if (Input.GetKeyDown(KeyCode.R))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = original_ball_position;
        }

    }



    void OnMouseDown()
    {
        LayerMask layerMask = LayerMask.GetMask("Ball_layer");

        mouse_on_screen = Input.mousePosition;

        // Ray from camera to mouse position on screen
        Ray ray = Camera.main.ScreenPointToRay(mouse_on_screen);

        // Check if the created ray hits something on the "Ball_layer", in this case the Ball. So, if the ball is kicked, continue forward.
        if (Physics.Raycast(ray, out RaycastHit hitData, layerMask))
        {
            mouse_world_position = hitData.point;

            Vector3 ball_location = transform.position;

            // Direction of the kick offset from the center of the ball
            Vector3 direction = (ball_location - mouse_world_position).normalized;

            // Reset the movement before adding the kick impact to the ball
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // The impact of the kick: Combination of force originated from kick offset and extra force making ball go upwards to ease the game and prevent downwards kick
            Vector3 force = direction * DIRECTIONAL_KICK_FORCE + Vector3.up * UPWARDS_KICK_FORCE;

            rb.AddForce(force, ForceMode.Impulse);

            // Add spin for the ball
            Vector3 torqueDir = Vector3.Cross(Vector3.up, direction);
            rb.AddTorque(torqueDir * SPIN_FORCE, ForceMode.Impulse);

            game_manager.UpdateScore();

        }


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Field")
        {
            game_manager.ResetScore();
        }

        //Ball bounce on the walls, BUGBUGBUG
        if (collision.collider.name == "LeftWall" || collision.collider.name == "RightWall")
        {
            Debug.Log(collision.collider.name);

            // Get the normal of the collision
            Vector3 normal = collision.contacts[0].normal;

            // Calculate the reflected velocity using Vector3.Reflect
            Vector3 reflectedVelocity = Vector3.Reflect(rb.linearVelocity, normal);
            Vector3 reflectedAngularVelocity = Vector3.Reflect(rb.angularVelocity, normal);

            // Apply the reflected velocity to the Rigidbody
            //rb.linearVelocity = reflectedVelocity * rb.linearVelocity.magnitude;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(reflectedVelocity, ForceMode.VelocityChange);
            rb.AddTorque(reflectedAngularVelocity, ForceMode.VelocityChange);
        }
    }

}

