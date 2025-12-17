using UnityEngine;

public class player_controller : MonoBehaviour
{
    public float move_speed = 20;
    public float jump_force = 5;
    public bool grounded = true;

    //

    private Vector3 movement;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Vairables /////////////////

        movement = Vector3.zero; // Reset movement

        /////////////////////////////
        
        // Grounded ///////////////////

        if (Physics.Raycast(transform.position,-Vector3.up,0.5f)) { grounded = true; }
        else { grounded = false; }

        ///////////////////////////////

        // Movement /////////////////////////

        if (Input.GetKey("w"))
        {
            movement += new Vector3(-move_speed,0,0);
        }
        if (Input.GetKey("a"))
        {
            movement += new Vector3(0,0,-move_speed);
        }
        if (Input.GetKey("s"))
        {
            movement += new Vector3(move_speed,0,0);
        }
        if (Input.GetKey("d"))
        {
            movement += new Vector3(0,0,move_speed);
        }
        if (Input.GetKey(KeyCode.Space) && grounded == true)
        {
            movement += new Vector3(0,jump_force,0);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            
        }

        movement += new Vector3(0,rb.linearVelocity.y,0); // Adds the current Y velocity onto our movement, for the jumping and gravity to work properly
        rb.linearVelocity = movement; // Turns our movement value into actual movement via linear velocity

        /////////////////////////////
    }
}
