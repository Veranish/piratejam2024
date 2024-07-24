using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour, IDamageable
{
    public int hp;
    public bool isAttacking;
    public Transform target;
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        //Call up to the GameState to find where to go

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed);
       
    }

    

    public void NewTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void Damage(int damageToTake)
    {
        hp -= damageToTake;
        CheckDeath();
        isAttacking = true;
        //Todo: Target the player
    }

    public bool CheckDeath()
    {
        if (hp < 0) 
        { 
            return true;
            
        }

        return false;
    }

    public void Die()
    {
        //Add sfx, death animation, timer, before the game object is destroyed.
        Destroy(this);

    }

}
