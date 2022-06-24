using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sasuke.Physics 
{
    public class Slammer
    {
        Rigidbody2D targetRigidbody;

        public Slammer(Rigidbody2D rigidbody) 
        {
            targetRigidbody = rigidbody;
        }

        public void Slam(Vector2 slamAmount = default(Vector2))
        {

            targetRigidbody.velocity = Vector2.zero;
            if (slamAmount.x != 0 || slamAmount.y != 0)
            {
                targetRigidbody.velocity = Vector2.zero;
                Vector2 movement =  Vector2.right* slamAmount.x + Vector2.up* slamAmount.y;
                targetRigidbody.AddForce(movement, ForceMode2D.Impulse);
            }
        }
    }
}

