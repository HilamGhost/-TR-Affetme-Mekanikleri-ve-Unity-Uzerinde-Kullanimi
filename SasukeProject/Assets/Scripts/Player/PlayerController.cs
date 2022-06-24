using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sasuke.Physics;
using Sasuke.Abstract;

namespace Sasuke.Player 
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Other Components")]
        PlayerManager _player;   
        CapsuleCollider2D playerCollider;
        PlayerStats playerStats;      
        PlayerInputManager playerInput;
        PlayerDash playerDash;

        public GroundChecker GroundChecker { get; private set; }
        public Rigidbody2D PlayerRB { get; private set; }



        [Header("Movement")]
        [SerializeField] float playerSpeed;
        float moveDirection;
        

        [Header("Jump")]
        [SerializeField] bool isGrounded;
        [SerializeField] float jumpSpeed;
        bool jumpPressed;
        
        [Header("Jump Forgive Mechs.")]
        [SerializeField] float jumpPressedRemember = 0.3f;
        float jumpPressedRemember_reset;
        [SerializeField] float groundedRemember = 0.3f;
        float groundedRemember_reset;
        [SerializeField, Range(0, 1)] float cutJumpHeight;
        [SerializeField, Range(0,10)] float minJumpRange;
        bool isJumpCutted = true;

        [Header("Dash")]
        [SerializeField] float DashSpeed;
        [SerializeField] float DashTime;
        bool isDashing;
      

        void Awake()
        {
            MakeAssignment();
        }


        void Update()
        {
            Timer();
            InputHandler();
            JumpStateManager();
            GetInputs();
        }
        private void FixedUpdate()
        {
            if(playerStats.PlayerState == PlayerStates.Idle 
                || playerStats.PlayerState == PlayerStates.InAir) 
            {
                Move(moveDirection);

                if (jumpPressedRemember > 0 && groundedRemember > 0)
                {
                    Jump();
                }
                if (isJumpCutted)
                {
                    CutJump();
                }
                if (isDashing) 
                {
                    if(moveDirection != 0) StartCoroutine(playerDash.Dash(DashSpeed * playerStats.Direction.x, DashTime));
                    isDashing = false;
                }
            }
            
            
            
        }

        void JumpStateManager() 
        {
            switch (playerStats.JumpState)
            {
                case JumpStates.PrepareJump:
                    jumpPressedRemember = jumpPressedRemember_reset;
                    isJumpCutted = false;

                    playerStats.JumpState = JumpStates.Jumping;
                    break;
               
                case JumpStates.Jumping:                                    
                    if(isJumpCutted) playerStats.JumpState = JumpStates.InAir;
                    if (isGrounded) playerStats.JumpState = JumpStates.Grounded;

                    break;
                case JumpStates.InAir:
                    if (isGrounded) playerStats.JumpState = JumpStates.Grounded;

                    break;
                case JumpStates.Grounded:                                                           
                    
                    if(isGrounded)
                        groundedRemember = groundedRemember_reset;
                    else 
                    {
                        if(groundedRemember <= 0) playerStats.JumpState = JumpStates.InAir;                     
                    }
                    break;
            }
        }
        void InputHandler()
        {
            moveDirection = playerStats.MoveDirection.x;
            isGrounded = GroundChecker.IsGrounded(this);
        }
        void GetInputs() 
        {
            if (playerInput.JumpButtonDown && playerStats.JumpState == JumpStates.Grounded 
                                           && playerStats.PlayerState == PlayerStates.Idle)
            {
                playerStats.JumpState = JumpStates.PrepareJump;

            }
            if (playerInput.JumpButtonUp)
            {
                isJumpCutted = true;
            }
            if (playerInput.DashButtonDown) 
            {
                isDashing = true;
            }
        }

        private void Move(float moveDirection)
        {
            float _horizontal = moveDirection * playerSpeed;                    
            PlayerRB.velocity = new Vector2(_horizontal, PlayerRB.velocity.y);
            
        }
        void Jump() 
        {
            PlayerRB.velocity = new Vector2(PlayerRB.velocity.x,jumpSpeed);
            jumpPressedRemember = 0;
            groundedRemember = 0;
        }
        void CutJump() 
        {
            if(PlayerRB.velocity.y > minJumpRange) 
            {
                PlayerRB.velocity = new Vector2(PlayerRB.velocity.x,PlayerRB.velocity.y*cutJumpHeight);
            }
        }

        

        void Timer() 
        {
            if(jumpPressedRemember > 0) jumpPressedRemember -= Time.deltaTime;            
            else jumpPressedRemember = 0;

            if (groundedRemember > 0) groundedRemember -= Time.deltaTime;
            else groundedRemember = 0;        

        }

        void MakeAssignment() 
        {
            _player = GetComponent<PlayerManager>();

            PlayerRB = _player.PlayerRB;
            playerCollider = _player.PlayerCollider;
            playerStats = _player.PlayerStats;
            playerInput = _player.PlayerInput;
            playerDash =  new PlayerDash(PlayerRB, playerStats);
            



            GroundChecker = GetComponentInChildren<GroundChecker>();


            #region Timer Resets
            jumpPressedRemember_reset = jumpPressedRemember;
            groundedRemember_reset = groundedRemember;
            #endregion
        }
        
        
    }

}
