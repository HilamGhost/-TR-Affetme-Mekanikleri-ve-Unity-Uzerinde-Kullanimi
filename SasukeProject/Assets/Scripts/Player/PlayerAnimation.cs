using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sasuke.Player 
{
    public class PlayerAnimation
    {
        Animator playerAnimator;
        PlayerStats playerStats;
        Rigidbody2D playerRb;

        bool isMoving;
        public PlayerAnimation(PlayerManager player)
        {
            playerAnimator = player.PlayerAnimator;
            playerStats = player.PlayerStats;
            playerRb = player.PlayerRB;
        }
        public bool IsMoving 
        {          
            set 
            {
                if (Mathf.Approximately(playerRb.velocity.x,0)) isMoving = false; else isMoving = true;
                if(isMoving) playerAnimator.SetFloat("Move",1); else playerAnimator.SetFloat("Move", 0);
            }
        }
        public bool isGrounded { set => playerAnimator.SetBool("isGrounded", playerStats.PlayerState == Abstract.PlayerStates.InAir); }
        public void Attack() { playerAnimator.SetTrigger("Attack"); }     
       
        public int ComboCount {  set => playerAnimator.SetInteger("ComboCount", value); }

        public void Dash() { playerAnimator.SetTrigger("Dash"); }
        
        public void Parry() { playerAnimator.SetTrigger("Parry"); }
        
      
    }
}


