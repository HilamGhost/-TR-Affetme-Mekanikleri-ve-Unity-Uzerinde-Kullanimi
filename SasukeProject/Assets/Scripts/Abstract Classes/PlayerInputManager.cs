using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sasuke.Abstract
{
    [CreateAssetMenu(fileName = "New Input Preset", menuName = "Managers/Input Preset")]
    public class PlayerInputManager : ScriptableObject
    {
        [SerializeField] string _horizontal = "Horizontal";
        [SerializeField] string _vertical = "Vertical";
        [SerializeField] string _jump = "Jump";
        [SerializeField] string _attack = "Fire1";
        [SerializeField] string _dash = "Dash";


        public float Horizontal { get => Input.GetAxisRaw(_horizontal); }
        public float Vertical { get => Input.GetAxisRaw(_vertical); }

        public bool JumpButtonDown { get => Input.GetButtonDown(_jump); }
        public bool JumpButtonUp   { get => Input.GetButtonUp(_jump); }

        public bool AttackButtonDown { get => Input.GetButtonDown(_attack); }
        public bool DashButtonDown { get => Input.GetButtonDown(_dash); }

    }
}

