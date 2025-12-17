using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class player_controller : MonoBehaviour
{
    public int health = 100;
    public float move_speed = 20;
    public float jump_force = 5;
    public bool grounded = true;
    public bool attacking = false;
    public bool enemy_hit = false;
    public AudioSource on_hit_audio_source;
    public AudioClip on_hit_audio_clip;

    //

    private bool enemy_hit_while_airborne = false;
    private bool attack_CD = false;
    private Renderer plr_renderer;
    private bool force_being_applied = false;
    Rigidbody rb;
    

    ////////////////////
    

    private IEnumerator movement_control_back()
    {
      plr_renderer.material.SetColor("_BaseColor", Color.black);
      yield return new WaitForSeconds(0.7f);
      force_being_applied = false;
      plr_renderer.material.SetColor("_BaseColor", Color.grey);
    }

    private IEnumerator bounce_time()
    {
      enemy_hit_while_airborne = true;
      yield return new WaitForSeconds(0.2f);
      enemy_hit_while_airborne = false;
    }

    private IEnumerator Attacking(bool put_attack_on_cd)
    {
        attacking = true;
        attack_CD = put_attack_on_cd;
        plr_renderer.material.SetColor("_BaseColor", Color.red);
        yield return new WaitForSeconds(0.25f);
        if (enemy_hit == true)
        {
            enemy_hit = false;
            yield return new WaitForSeconds(0.25f);
            if (enemy_hit == true) 
            {
                // Run code again!
                enemy_hit = false;
                StartCoroutine(Attacking(false)); //Restarts it
                yield break; 
            } 
            attacking = false;
            attack_CD = false;
            plr_renderer.material.SetColor("_BaseColor", Color.grey);
            yield break;
        }
        attacking = false;
        plr_renderer.material.SetColor("_BaseColor", Color.grey);
        yield return new WaitForSeconds(2.75f); // If your on CD
        attack_CD = false;
        
    }



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        plr_renderer = GetComponent<Renderer>();
    }

    public void take_damage(Transform other_object_transform, int damage_value)
    {
        on_hit_audio_source.PlayOneShot(on_hit_audio_clip); // Plays our audio
        health -= damage_value; // Takes HP
        if (health <= 0) { SceneManager.LoadScene( SceneManager.GetActiveScene().name );} // Restarts scene if dead
        Vector3 player_direction = transform.position - other_object_transform.position; // Our pushback direction
        force_being_applied = true; // Stops us from moving
        rb.AddForce(player_direction * 300,ForceMode.Acceleration); /// Applies force
        StartCoroutine(movement_control_back()); // Cleans up so we can move again
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Vairables /////////////////

        Vector3 movement = Vector3.zero; // Set movement

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

        if (force_being_applied == false)
        {
            movement.y += rb.linearVelocity.y; // Adds the current Y velocity onto our movement, for the jumping and gravity to work properly
            if (enemy_hit_while_airborne == true) {movement.y = 5; }
            rb.linearVelocity = movement; // Turns our movement value into actual movement via linear velocity
        }
        

        /////////////////////////////
        /// 
        /// ATTACK MECHANIC
        
         if (Input.GetKey(KeyCode.F) && attack_CD == false && attacking == false && force_being_applied == false)
        {
            StartCoroutine(Attacking(true));
        }

        ////////////////////////////////////////
    }
}
