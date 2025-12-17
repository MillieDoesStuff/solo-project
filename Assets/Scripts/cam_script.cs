using UnityEngine;

public class cam_script : MonoBehaviour
{
    public Transform player_transform;

    void FixedUpdate()
    {
        transform.position = new Vector3(player_transform.position.x+3,player_transform.position.y+8,player_transform.position.z); // Keeps the camera fixed to the player
    }
}
