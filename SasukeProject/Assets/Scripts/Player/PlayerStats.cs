using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sasuke.Abstract;

namespace Sasuke.Player 
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("Other Components")]
        PlayerManager _player;
        PlayerInputManager playerInput;
        public PlayerAnimation PlayerAnimation { get; private set; }

        [Header("States")]
        [SerializeField] PlayerStates playerState;
        [SerializeField] JumpStates jumpState;

        [Header("Stats")]
        [SerializeField] int health;
        [SerializeField] Vector2 direction;

        #region Properties
        public PlayerStates PlayerState
        {
            get { return playerState; }
            private set { playerState = value; }
        }
        public JumpStates JumpState
        {
            get { return jumpState; }
            set
            {
                jumpState = value;
                if (value != JumpStates.Grounded) PlayerState = PlayerStates.InAir;
                if (value == JumpStates.Grounded && PlayerState == PlayerStates.InAir) PlayerState = PlayerStates.Idle;
            }
        }

        public Vector2 Direction
        {
            get { return direction; }
            private set { if (value.x != 0) direction = value; }
        }
        public Vector2 MoveDirection
        {
            get;
            private set;
        }
        public bool CanChangeDirection 
        {
            get 
            {
                if (PlayerState == PlayerStates.Idle || PlayerState == PlayerStates.InAir) return true; else return false;                      
            } 
        }
        #endregion

        void Start()
        {
            MakeAssignment();
        }


        void Update()
        {
            if(CanChangeDirection) Direction = new Vector2(playerInput.Horizontal, playerInput.Vertical);
            if(CanChangeDirection) MoveDirection = new Vector2(playerInput.Horizontal, playerInput.Vertical);

            if (direction.x != 0 && CanChangeDirection) transform.localScale = new Vector3(direction.x, 1, 1);

            PlayerAnimation.IsMoving = true;
            PlayerAnimation.isGrounded = true;
        }

        void MakeAssignment()
        {
            _player = GetComponent<PlayerManager>();
            PlayerAnimation = new PlayerAnimation(_player);

            playerInput = _player.PlayerInput;
        }
        public void ChangeLayer(string layer)
        {
            switch (layer)
            {
                case "Player Immune": gameObject.layer = 8; break;
                case "Player": gameObject.layer = 6; break;
            }
        }
        public void ChangeState(string state)
        {
            switch (state)
            {
                case "Idle": PlayerState = PlayerStates.Idle; break;
                case "InAir": PlayerState = PlayerStates.InAir; break;
                case "Attacking": PlayerState = PlayerStates.Attacking; break;
                case "Dash": PlayerState = PlayerStates.Dash; break;

            }

        }
    }
    
}

