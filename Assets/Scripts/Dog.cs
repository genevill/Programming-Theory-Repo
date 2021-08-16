using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Animal
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private void Update()
    {
        Move(); //Inheritance
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump(); //Inheritance
        }
    }

    protected override void Jump() //Polymorphism
    {
        JumpForce = 10; 
        base.Jump();
    }
}
