using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour, IDamageable
{
    public int hp;
    public bool isAttacking;
    public Transform target;
    public int speed;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
        if(transform.position.y < 0.5f || transform.position.y > 30)
        {
            transform.position = new Vector3(transform.position.x, 1.3f, transform.position.z);
        }
       
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CarriageAnimateSpline>(out CarriageAnimateSpline carriage))
        {
            Debug.Log("Carriage damage from Shadow! Destroying self");
            carriage.Damage(damage);
            Destroy(this.gameObject);
        }
    }

    

    public void NewTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void Damage(int damageToTake)
    {
        hp -= damageToTake;
        if(CheckDeath())
        {
            Destroy(this.gameObject);
        }
        isAttacking = true;
        //Todo: Target the player
    }

    public bool CheckDeath()
    {
        if (hp < 0) 
        {
            Debug.Log("Shadow killed!");
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
