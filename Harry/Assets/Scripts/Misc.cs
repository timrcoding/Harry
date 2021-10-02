using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class Misc : MonoBehaviour
{
    
}

public class StringValue : System.Attribute
{
    private readonly string _value;

    public StringValue(string value)
    {
        _value = value;
    }

    public string Value
    {
        get { return _value; }
    }

}

public static class StringEnum
{
    public static string GetStringValue(Enum value)
    {
        string output = null;
        Type type = value.GetType();
        FieldInfo fi = type.GetField(value.ToString());
        StringValue[] attrs =
           fi.GetCustomAttributes(typeof(StringValue),
                                   false) as StringValue[];
        if (attrs.Length > 0)
        {
            output = attrs[0].Value;
        }

        return output;
    }
}
