using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sasuke.Player;
namespace Sasuke.Physics
{
    public class GroundChecker : MonoBehaviour
    {

        bool isGrounded;
        [SerializeField] LayerMask groundLayer;
        public LayerMask GroundLayer { get => groundLayer; }


        public bool IsGrounded() 
        {
            return isGrounded;
        }
        public bool IsGrounded(PlayerController playerController)
        {

            if (Mathf.Approximately(playerController.PlayerRB.velocity.y, 0))
            {
                return isGrounded;
            }
            else
            {
                return false;
            }

        }
       
        private void OnTriggerStay2D(Collider2D collision)
        {
            isGrounded = collision != null && (((1 << collision.gameObject.layer) & groundLayer) != 0);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            isGrounded = false;
        }
    }
}

