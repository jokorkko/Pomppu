using System;
using UnityEngine;



public class Pallo : MonoBehaviour
{

    public const float KICK_FORCE = 2f;
    private Rigidbody rb;

    public Vector3 mouse_on_screen;
    public Vector3 mouse_world_position;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void OnMouseDown()
    {

        mouse_on_screen = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouse_on_screen);

        if (Physics.Raycast(ray, out RaycastHit hitData)){
            mouse_world_position = hitData.point;
        }

        Debug.Log("World Position: " + mouse_world_position);

        Vector3 ball_location = transform.position;
        Debug.Log("Pallon lokaatio: " + ball_location);

        Vector3 kick_offset = ball_location - mouse_world_position;
        Debug.Log("Offset: " + kick_offset);

        //rb.linearVelocity = Vector3.zero;
        //rb.AddForce(kick_offset * KICK_FORCE, ForceMode.Impulse);


        //Nollaa nopeus ja lisää voimaa ylöspäin
        rb.linearVelocity = Vector3.zero;
        rb.AddForce((Vector3.up + kick_offset * 3f) * KICK_FORCE, ForceMode.Impulse);
    }*/

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
    rb.AddTorque(torqueDir * KICK_FORCE, ForceMode.Impulse);
}

}

