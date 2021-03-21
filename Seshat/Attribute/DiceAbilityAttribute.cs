namespace Seshat.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class DiceAbilityAttribute : System.Attribute
    {
        public string id;

        public DiceAbilityAttribute(string id)
        {
            this.id = id;
        }
    }
}
