using System;
using System.Reflection;

namespace ATInspector
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class ATButtonAttribute : Attribute
    {
        string buttonName;
        public string ButtonName => buttonName;

        MethodInfo method;
        public MethodInfo Method => method;
        public void SetButtonMethod(MethodInfo methodInfo) => method = methodInfo;

        public ATButtonAttribute(string buttonName)
        {
            this.buttonName = buttonName;
            method = null;
        }
    }
}

