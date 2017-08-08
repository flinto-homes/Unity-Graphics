﻿using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Experimental.UIElements.StyleEnums;
using UnityEditor.Experimental.UIElements;

namespace UnityEditor.VFX.UIElements
{
    class UintField : ValueControl<uint>, IValueChangeListener<uint>
    {
        EditorTextField m_TextField;

        void CreateTextField()
        {
            m_TextField = new EditorTextField(30, false, false, '*');
            m_TextField.AddToClassList("textfield");
            m_TextField.OnTextChanged = OnTextChanged;
        }

        public UintField(string label) : base(label)
        {
            CreateTextField();
            m_Label.AddManipulator(new DragValueManipulator<uint>(this, null));

            style.flexDirection = FlexDirection.Row;
            Add(m_TextField);
        }

        public UintField(VisualElement existingLabel) : base(existingLabel)
        {
            CreateTextField();
            Add(m_TextField);

            if (m_Label != null)
                m_Label.AddManipulator(new DragValueManipulator<uint>(this, null));
        }

        void OnTextChanged(string str)
        {
            m_Value = 0;
            uint.TryParse(m_TextField.text, out m_Value);

            if (OnValueChanged != null)
            {
                OnValueChanged();
            }
        }

        uint IValueChangeListener<uint>.GetValue(object userData)
        {
            uint newValue = 0;
            uint.TryParse(m_TextField.text, out newValue);
            return newValue;
        }

        void IValueChangeListener<uint>.SetValue(uint value, object userData)
        {
            m_Value = value;
            ValueToGUI();

            if (OnValueChanged != null)
            {
                OnValueChanged();
            }
        }

        protected override void ValueToGUI()
        {
            m_TextField.text = m_Value.ToString("0.###");
        }
    }
}
