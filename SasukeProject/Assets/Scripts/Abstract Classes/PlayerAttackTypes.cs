using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sasuke.Abstract 
{
    [CreateAssetMenu(menuName = "Attack Types/ Create Attack Types", fileName = "New Attack Type")]
    public class PlayerAttackTypes : ScriptableObject
    {
        [SerializeField] float attackRate;
        [SerializeField] Vector2 slamRate;
        [SerializeField] float slamTime;
        public float AttackRate { get => attackRate; }
        public Vector2 SlamRate { get => slamRate; }
        public float SlamTime { get => slamTime; }
    }
}

