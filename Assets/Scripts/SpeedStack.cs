using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedStack : MonoBehaviour
{
    public float stackAmount = 5;
    public float stackTime = 0;
    
    
    

    void Start()
    {
        
    }


    void Update()
    {
        stackDebug();
        stackCheck();

    }


    public void stackAdd()
    {
        //fires off in EnemyParent death

        stackTime = 10;

        if (stackAmount >= 8)
            return;
        else
        {
            stackAmount += .25f;
            Debug.Log("SPEED UP");
        }
    }

    public void stackCheck()
    {
        //goes in the update

        PlayerControls.moveSpeed = stackAmount;

            

        if (stackTime > 0)
        {
            stackTime -= Time.deltaTime;
            Debug.Log("Time:" + stackTime);
        }

        else
        {
            stackTime = 0;
            stackAmount = 5;
        }




    }

    public void stackDebug()
    {
        //goes in the update

        if (Input.GetKeyDown(KeyCode.H))
        {
            stackAdd();
        }
        
  
    }
}
