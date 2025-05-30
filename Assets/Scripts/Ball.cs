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

    //public GameManager game_manager;
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

        left_wall = GameObject.Find("LeftWall");
        right_wall = GameObject.Find("RightWall");

        //Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

        float distance = Vector3.Distance(Camera.main.transform.position, transform.position);

        Vector3 left_position = camera.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 right_position = camera.ViewportToWorldPoint(new Vector3(1, 0, distance));

        left_wall.transform.position = left_position;
        right_wall.transform.position = right_position;//Toteuta kimpoaminen
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



        /*if (pos.x < -0.3 || 1.3 < pos.x)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = original_ball_position;
        }*/

    }



    void OnMouseDown()
    {
        mouse_on_screen = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouse_on_screen);

        if (Physics.Raycast(ray, out RaycastHit hitData))
        {
            mouse_world_position = hitData.point;
        }

        Vector3 ball_location = transform.position;

        // Direction of the kick offset
        Vector3 direction = (ball_location - mouse_world_position).normalized;

        // Reset the movement
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // The impact of the kick: Combination of force originated from kick offset and extra force making ball go upwards to ease the game and prevent downwards kick
        Vector3 force = direction * DIRECTIONAL_KICK_FORCE + Vector3.up * UPWARDS_KICK_FORCE;


        rb.AddForce(force, ForceMode.Impulse);

        // Add spin for the ball
        Vector3 torqueDir = Vector3.Cross(Vector3.up, direction);
        rb.AddTorque(torqueDir * SPIN_FORCE, ForceMode.Impulse);

        score += 1;
        if (score > high_score)
        {
            high_score = score;
            ui.UpdateBestScore(high_score);
        }

        ui.UpdateScore(score);

    }

    void OnCollisionEnter(Collision collision)
    {
        score = 0;
        ui.UpdateScore(score);
    }

}

