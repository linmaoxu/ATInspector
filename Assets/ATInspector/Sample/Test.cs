using System;
using ATInspector;
using UnityEngine;

public class Test : MonoBehaviour
{
    [ATEnumToggleButton("攻击方式")]
    public AttackType attackType;

    [ATButtonAttribute("TestButton")]
    public void TestButton()
    {
        Debug.Log("按钮测试完成");
    }
}

public enum AttackType{
    远程攻击=0,
    近战攻击=1,
    中距离攻击=2
}