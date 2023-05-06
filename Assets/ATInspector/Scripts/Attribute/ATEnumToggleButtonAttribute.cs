using System;
using System.Reflection;

namespace ATInspector
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class ATEnumToggleButtonAttribute : Attribute
    {
        string label;
        public string Label => label;

        FieldInfo belongField;
        public FieldInfo BelongField => belongField;
        public void SetBelongField(FieldInfo belongField) => this.belongField = belongField;

        int buttonWidth;
        public int ButtonWidth => buttonWidth;

        int buttonHeight;
        public int ButtonHeight => buttonHeight;

        public ATEnumToggleButtonAttribute(string label)
        {
            this.label = label;
            buttonHeight = 30;
            buttonWidth = 105;
        }

        public ATEnumToggleButtonAttribute(string label, int buttonWidth, int buttonHeight)
        {
            this.label = label;
            this.buttonHeight = buttonHeight;
            this.buttonWidth = buttonWidth;
        }

        public bool IsContainValue(object target, int value)
        {
            if (belongField == null) return false;

            if (belongField.FieldType.IsEnum)
            {
                if (belongField.FieldType.IsDefined(typeof(System.FlagsAttribute), false))
                {
                    return ((int)belongField.GetValue(target) & value) == value;
                }
                else
                {
                    return (int)belongField.GetValue(target) == value;
                }
            }
            return false;
        }

        public void SetEnumValue(object target, int value)
        {
            if (belongField.FieldType.IsEnum)
            {
                if (belongField.FieldType.IsDefined(typeof(System.FlagsAttribute), false))
                {
                     int currentValue = (int)belongField.GetValue(target);
                    if (IsContainValue(target,value))
                    {
                        currentValue -= value;
                    }
                    else
                    {
                        currentValue += value;
                    }                  
                    belongField.SetValue(target, currentValue);
                }
                else
                {
                    belongField.SetValue(target, value);
                }
            }
        }

    }
}