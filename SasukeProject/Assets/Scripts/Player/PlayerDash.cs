using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sasuke.Player 
{
    public class PlayerDash 
    {
        PlayerStats playerStats;
        Rigidbody2D playerRb;
        
        public PlayerDash(Rigidbody2D rb, PlayerStats playerStat) 
        {
            playerRb = rb;
            playerStats = playerStat;
        }

        public IEnumerator Dash(float dashSpeed, float dashTime) 
        {
            playerRb.velocity = Vector2.zero;

            playerStats.ChangeLayer("Player Immune");
            playerStats.ChangeState("Dash");

            playerStats.PlayerAnimation.Dash();
            playerRb.velocity = new Vector2(dashSpeed, playerRb.velocity.y);
            yield return new WaitForSeconds(dashTime);
            
            playerStats.ChangeLayer("Player");
            playerStats.ChangeState("Idle");
        }
    }
}

