using System;


namespace InvertXMouse.Redirection
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RedirectMethodAttribute : Attribute
    {
        public bool OnCreated { get; }


        public RedirectMethodAttribute()
        {
            OnCreated = false;
        }

        public RedirectMethodAttribute(bool onCreated)
        {
            OnCreated = onCreated;
        }
    }
}
