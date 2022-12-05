using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : PlayerManagement {

    PlayerManagement playerManagement = new PlayerManagement();

    public void Footstep(AudioSource playerFootstep){
        playerFootstep.Play();
    }
}
