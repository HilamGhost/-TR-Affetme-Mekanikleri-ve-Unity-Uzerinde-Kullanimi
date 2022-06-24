using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sasuke.Abstract;

namespace Sasuke.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Other Components")]
        PlayerManager _player;
        PlayerStats playerStats;
        Rigidbody2D playerRB;
        Physics.Slammer slammer;

        [Header("Player Stats")]       
        [SerializeField] bool isAttacking;
        [SerializeField] int comboCount;
        [SerializeField] int maxComboCount = 4;
        float AttackDirection { get => playerStats.Direction.x; }

        [SerializeField, Range(0, 2)] float pressRemember;
        float _pressRememberReset;

        [Header("AttackTypes")]
        [SerializeField] List<PlayerAttackTypes> attackTypes;
        [SerializeField] PlayerAttackTypes airAttackType;



        void Awake()
        {
            MakeAssignment();
        }

        void Update()
        {
            if (!isAttacking) 
            {
                if (playerStats.PlayerState == PlayerStates.InAir)
                {
                    if (_player.PlayerInput.AttackButtonDown)
                    {
                        StartCoroutine(AirAttack());
                    }
                }
                else
                {
                    if (comboCount == 0)
                    {
                        if (playerStats.PlayerState == PlayerStates.Idle)
                        {

                            if (_player.PlayerInput.AttackButtonDown)
                            {
                                StartCoroutine(Attack());

                                comboCount++;

                            }

                        }


                    }
                    else if (comboCount > 0)
                    {
                        if (pressRemember > 0  && comboCount < maxComboCount)
                        {
                            if (_player.PlayerInput.AttackButtonDown)
                            {
                                StartCoroutine(Attack());
                                comboCount++;
                            }
                        }
                        else if (comboCount > maxComboCount)
                        {
                            comboCount = 0;
                            pressRemember = _pressRememberReset;
                        }
                    }
                }
            }
            
           

            if(playerStats.PlayerState == PlayerStates.Attacking) 
            {
                if (pressRemember > 0)
                {
                    pressRemember -= Time.deltaTime;
                }
                else
                {
                    pressRemember = 0;
                    comboCount = 0;
                    if (Mathf.Approximately(playerRB.velocity.y, 0)) playerStats.ChangeState("Idle");
                }
            }
            

            playerStats.PlayerAnimation.ComboCount = comboCount;

        }
        IEnumerator Attack()
        {
            isAttacking = true;
            PlayerAttackTypes _currentAttack = attackTypes[comboCount];
            pressRemember = _pressRememberReset;

            Debug.Log("Attack");
            playerStats.ChangeState("Attacking");

            playerStats.PlayerAnimation.Attack();

            yield return new WaitForSeconds(_currentAttack.SlamTime);
            slammer.Slam(_currentAttack.SlamRate * AttackDirection);

            yield return new WaitForSeconds(_currentAttack.AttackRate);

            slammer.Slam();

            isAttacking = false;

        }
        IEnumerator AirAttack()
        {
            isAttacking = true;
            PlayerAttackTypes _currentAttack = airAttackType;

            Debug.Log("Air Attack");

            playerStats.PlayerAnimation.Attack();
            yield return new WaitForSeconds(_currentAttack.AttackRate);
            isAttacking = false;
        }
        
        void MakeAssignment()
        {
            _player = GetComponent<PlayerManager>();

            playerStats = _player.PlayerStats;
            _pressRememberReset = pressRemember;
            playerRB = _player.PlayerRB;
            slammer = new Physics.Slammer(playerRB);
        }

    }
}
   

