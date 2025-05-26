using System;
using UnityEngine;



public class Pallo : MonoBehaviour
{

    public const float KICK_FORCE = 3f;
    public const float SPIN_FORCE = 0.5f;
    private Rigidbody rb;

    public Vector3 mouse_on_screen;
    public Vector3 mouse_world_position;
    public Vector3 original_ball_position;

    //int testi = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        original_ball_position = rb.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        //reset ball position and movement when "R" is pressed on keyboard and
        if (Input.GetKeyDown(KeyCode.R))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = original_ball_position;
        }
        
    }

   

    void OnMouseDown()
{
    mouse_on_screen = Input.mousePosition;
    Ray ray = Camera.main.ScreenPointToRay(mouse_on_screen);

    if (Physics.Raycast(ray, out RaycastHit hitData)){
        mouse_world_position = hitData.point;
    }

    Vector3 ball_location = transform.position;

    // Suunta pallon keskustasta klikkauspisteeseen (pallon reunaan)
    Vector3 direction = (ball_location - mouse_world_position).normalized;

    // Nollaa liike
    rb.linearVelocity = Vector3.zero;
    rb.angularVelocity = Vector3.zero;

    // Voima: yhdistelmä vaakasuoraa potkua ja pystysuuntaista pomppua
    Vector3 force = direction * KICK_FORCE + Vector3.up * KICK_FORCE;

    
    rb.AddForce(force, ForceMode.Impulse);
    

    

    // Lisää realistinen pyörimisliike
    Vector3 torqueDir = Vector3.Cross(Vector3.up, direction);
    rb.AddTorque(torqueDir * SPIN_FORCE, ForceMode.Impulse);
    
}

}

