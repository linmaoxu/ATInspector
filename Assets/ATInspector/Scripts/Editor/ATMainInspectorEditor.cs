using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ATInspector
{
    public class ATMainInspectorEditor : Editor
    {
        public List<ATButtonAttribute> buttons;
        public List<ATEnumToggleButtonAttribute> enumButtons;

        public void OnEnable()
        {
            //----------Button---------
            buttons = new List<ATButtonAttribute>();
            MethodInfo[] methods = target.GetType().GetMethods();

            foreach (var method in methods)
            {
                ATButtonAttribute button = (ATButtonAttribute)method.GetCustomAttributes(typeof(ATButtonAttribute), true).FirstOrDefault();

                if (button != null)
                {
                    button.SetButtonMethod(method);
                    buttons.Add(button);
                }
            }

            //----------Field---------------
            enumButtons = new List<ATEnumToggleButtonAttribute>();
            FieldInfo[] fieldInfos = target.GetType().GetFields();

            foreach (var fieldInfo in fieldInfos)
            {
                var attributes = fieldInfo.GetCustomAttributes(true);
                foreach (var attribute in attributes)
                {
                    //EnumButton
                    if (attribute is ATEnumToggleButtonAttribute)
                    {
                        var attr = attribute as ATEnumToggleButtonAttribute;
                        attr.SetBelongField(fieldInfo);
                        enumButtons.Add(attr);
                    }
                }
            }
        }

        public void OnDisable()
        {
            buttons?.Clear();
            enumButtons?.Clear();
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
                        button.Method.Invoke(target, null);
                    }
                }
            }

            //---------EnumButton----------
            if (enumButtons != null)
            {
                foreach (var enumButton in enumButtons)
                {
                    GUILayout.Label(enumButton.Label);

                    var values = Enum.GetValues(enumButton.BelongField.FieldType);
                    int length = values.Length;

                    for (int i = 0; i < length; i++)
                    {
                        if (i % 4 == 0)
                        {
                            GUILayout.BeginHorizontal();
                        }

                        int value = (int)values.GetValue(i);
                        string name = Enum.GetName(enumButton.BelongField.FieldType, value);
                        GUIStyle myGUIStyle = new GUIStyle(GUI.skin.button);
                        GUI.backgroundColor = enumButton.IsContainValue(target, value) ? Color.cyan : Color.white;
                        if (enumButton.IsContainValue(target, value))
                        {
                            myGUIStyle.normal.textColor = Color.yellow;
                        }

                        if (GUILayout.Button(name, myGUIStyle, GUILayout.Width(enumButton.ButtonWidth), GUILayout.Height(enumButton.ButtonHeight)))
                        {
                            enumButton.SetEnumValue(target, value);
                        }

                        if (i % 4 == 3 || i == length-1)
                        {
                            GUILayout.EndHorizontal();
                        }

                        GUI.backgroundColor = Color.white;
                    }

                }
            }
        }
    }

    [CustomEditor(typeof(MonoBehaviour), editorForChildClasses: true, isFallback = true)]
    public class ATMonoBehaviourInspector : ATMainInspectorEditor
    {

    }

    [CustomEditor(typeof(ScriptableObject), editorForChildClasses: true, isFallback = true)]
    public class ATScriptableObjectInspector : ATMainInspectorEditor
    {

    }
}