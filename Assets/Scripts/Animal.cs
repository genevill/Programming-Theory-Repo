using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    private float range = 19;
    private Rigidbody animalRb;
    private NavMeshAgent animalNav;
    private Vector3 position;
    private bool animalCollsion = false;
    private bool isJumping = false;
    private float m_JumpForce = 5;
    protected float JumpForce
    {
        get { return m_JumpForce; }
        set 
        { 
            if (value < 1)
            {
                Debug.LogError("You can't set a jump force less than 1.");
            }
            else
            {
                m_JumpForce = value;
            }
        }
    } //Encapsulation

    protected virtual void Start()
    {
        animalRb = GetComponent<Rigidbody>();
        animalNav = GetComponent<NavMeshAgent>();
    }

    protected void Move()
    {
        if (position == null || animalNav.destination == null || transform.position == position || animalCollsion)
        {
            position = RandomizePosition(range);
            animalCollsion = false;
            Debug.Log($"Target for {gameObject.name} is {animalNav.destination}");
        }
        else
        {
            if(!isJumping)
            {
                animalNav.SetDestination(position);
            }
        }
    }

    protected Vector3 RandomizePosition(float range)
    {
        float randX = Random.Range(-range, range);
        float randZ = Random.Range(-range, range);
        return new Vector3(randX, transform.position.y, randZ);
    }

    protected virtual void Jump()
    {
        JumpNav(); //Abstraction
    }

    protected void JumpNav()
    {
        animalNav.enabled = false;
        animalNav.ResetPath();
        animalRb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        isJumping = true;
    }

    protected void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Animal"))
        {
            animalCollsion = true;
        }

        if (other.gameObject.CompareTag("Ground") && isJumping)
        {
            animalNav.enabled = true;
            isJumping = false;
        }
    }
}
