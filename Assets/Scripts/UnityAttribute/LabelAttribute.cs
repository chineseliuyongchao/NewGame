using UnityEngine;

namespace UnityAttribute
{
    public class LabelAttribute : PropertyAttribute
    {
        public LabelAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}