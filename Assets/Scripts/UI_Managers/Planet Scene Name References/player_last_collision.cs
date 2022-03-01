using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class player_last_collision : NetworkBehaviour
{
    //set through the script PlanetCollision
    //used by the scene_manager to pick which scene to load
    private string last_planet_collide;
    private Transform last_player_position;

    public void set_last_planet_collide(string in_name)
    {
        last_planet_collide = in_name;
    }
    public string get_last_planet_collide()
    {
        return last_planet_collide;
    }

    public void set_last_player_pos(Transform in_pos)
    {
        last_player_position = in_pos;
    }
    public Transform get_last_player_pos()
    {
        return last_player_position;
    }
}
