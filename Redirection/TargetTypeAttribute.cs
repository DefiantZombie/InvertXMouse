using System;


namespace InvertXMouse.Redirection
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class TargetTypeAttribute : Attribute
    {
        public Type Type { get; }


        public TargetTypeAttribute(Type type)
        {
            Type = type;
        }
    }
}
