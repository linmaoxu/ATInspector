using ATInspector;
using UnityEngine;

public class Test : MonoBehaviour
{
    [ATButtonAttribute("TestButton")]
    public void TestButton()
    {
        Debug.Log("按钮测试完成");
    }
}
