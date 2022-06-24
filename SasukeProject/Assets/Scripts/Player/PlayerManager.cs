using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sasuke.Abstract;

namespace Sasuke.Player 
{
    public class PlayerManager : MonoBehaviour
    {
        /// <summary>
        /// Other Components
        /// </summary>
        public Rigidbody2D PlayerRB { get => GetComponent<Rigidbody2D>(); }
        public CapsuleCollider2D PlayerCollider { get => GetComponent<CapsuleCollider2D>(); }
        public Animator PlayerAnimator { get => GetComponent<Animator>(); }


        /// <summary>
        /// Other Player Component
        /// </summary>
        [SerializeField] PlayerInputManager _playerInput;
        public PlayerInputManager PlayerInput { get { return _playerInput; } }
        public PlayerController PlayerController { get => GetComponent<PlayerController>(); }
        public PlayerStats PlayerStats { get => GetComponent<PlayerStats>(); }
    }
}

