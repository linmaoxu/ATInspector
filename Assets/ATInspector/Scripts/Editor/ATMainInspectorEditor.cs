using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ATInspector
{
    public class ATMainInspectorEditor : Editor
    {
        public List<ATButtonAttribute> buttons;

        public void OnEnable()
        {
            //----------Button---------
            buttons = new List<ATButtonAttribute>();
            MethodInfo[] methods = target.GetType().GetMethods();

            foreach (var method in methods)
            {
                ATButtonAttribute button = (ATButtonAttribute)method.GetCustomAttributes(typeof(ATButtonAttribute),true).FirstOrDefault();

                if (button != null)
                {
                    button.SetButtonMethod(method);
                    buttons.Add(button);
                }
            }


        }

        public void OnDisable()
        {
            buttons?.Clear();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
           // const BindingFlags BINDINGFLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

             //----------Button---------
            if (buttons != null)
            {
                foreach (var button in buttons)
                {
                    if (GUILayout.Button(button.ButtonName))
                    {
                        button.Method.Invoke(target,null);
                    }
                }
            }
        }
    }

    [CustomEditor(typeof(MonoBehaviour), editorForChildClasses: true, isFallback = true)]
    public class ATMonoBehaviourInspector:ATMainInspectorEditor
    {

    }

    [CustomEditor(typeof(ScriptableObject), editorForChildClasses: true, isFallback = true)]
    public class ATScriptableObjectInspector:ATMainInspectorEditor
    {

    }
}