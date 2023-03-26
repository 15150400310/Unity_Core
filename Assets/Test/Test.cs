using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum TestStateType
{
    A,
    B,
    C,
    D,
}
public class TestStateBase : StateBase
{
    protected Text text;
    public override void Init(IStateMachineOwner owner, int stateType, StateMachine stateMachine)
    {
        base.Init(owner, stateType, stateMachine);
        text = (owner as Test).text;
    }

    public override void UnInit()
    {
        base.UnInit();
        text = null;
        Debug.Log("UnInit");
    }
}
[Pool]
public class Test_A: TestStateBase
{
    public override void Init(IStateMachineOwner owner, int stateType, StateMachine stateMachine)
    {
        base.Init(owner, stateType, stateMachine);
        Debug.Log("A_Init");
    }

    public override void Enter()
    {
        text.text = "A";
    }

    public override void Update()
    {
        Debug.Log("A_Update");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState<Test_B>((int)TestStateType.B);
        }
    }
}
[Pool]
public class Test_B : TestStateBase
{
    public override void Init(IStateMachineOwner owner, int stateType, StateMachine stateMachine)
    {
        base.Init(owner, stateType, stateMachine);
        Debug.Log("B_Init");
    }

    public override void Enter()
    {
        text.text = "B";
    }

    public override void Update()
    {
        Debug.Log("B_Update");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState<Test_C>((int)TestStateType.C);
        }
    }
}
[Pool]
public class Test_C : TestStateBase
{
    public override void Init(IStateMachineOwner owner, int stateType, StateMachine stateMachine)
    {
        base.Init(owner, stateType, stateMachine);
        Debug.Log("C_Init");
    }

    public override void Enter()
    {
        text.text = "C";
    }

    public override void Update()
    {
        Debug.Log("C_Update");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState<Test_D>((int)TestStateType.D);
        }
    }
}
[Pool]
public class Test_D : TestStateBase
{
    public override void Init(IStateMachineOwner owner, int stateType, StateMachine stateMachine)
    {
        base.Init(owner, stateType, stateMachine);
        Debug.Log("D_Init");
    }

    public override void Enter()
    {
        text.text = "D";
    }

    public override void Update()
    {
        Debug.Log("D_Update");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState<Test_A>((int)TestStateType.A);
        }
    }
}

public class Test : MonoBehaviour,IStateMachineOwner
{
    public Text text { get; private set; }
    StateMachine stateMachine;
    void Start()
    {
        text = GetComponent<Text>();
        stateMachine = ResManager.Load<StateMachine>();
        stateMachine.Init(this);
        stateMachine.ChangeState<Test_A>((int)TestStateType.A);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            stateMachine.ChangeState<Test_A>((int)TestStateType.A);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            stateMachine.Stop();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            stateMachine.Destroy();
            stateMachine = null;
        }
    }
}
