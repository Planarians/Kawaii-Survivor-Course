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
        myAction?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DebugANumber()
    {
        Debug.Log("5");
    }

    private void DebugAString()
    {
        Debug.Log("Hello");
    }
}
