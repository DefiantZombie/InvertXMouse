using System;


namespace InvertXMouse.Redirection
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RedirectReversedAttribute : Attribute
    {
        public bool OnCreated { get; }


        public RedirectReversedAttribute()
        {
            OnCreated = false;
        }


        public RedirectReversedAttribute(bool onCreated)
        {
            OnCreated = onCreated;
        }
    }
}
