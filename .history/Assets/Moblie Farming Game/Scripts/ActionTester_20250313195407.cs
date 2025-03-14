using System;
using UnityEngine;

public class ActionTester : MonoBehaviour
{

    public Action<int> myAction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myAction = DebugANumber;
        myAction += DebugAString;

        // if (myAction != null)
        // {
        //     myAction();
        // }
        //和上面注释的等价
        myAction?.Invoke(7);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InscreaseNumber(int number)
    {
        number++;
        Debug.Log(number);
    }

    private void DoubleNumber(int number)
    {
        number *= 2;
        Debug.Log(number);
    }

    // private void DebugANumber()
    // {
    //     Debug.Log("5");
    // }

    // private void DebugAString()
    // {
    //     Debug.Log("Hello");
    // }
}
