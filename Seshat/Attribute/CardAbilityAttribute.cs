namespace Seshat.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class CardAbilityAttribute : System.Attribute
    {
        public string id;

        public CardAbilityAttribute(string id)
        {
            this.id = id;
        }
    }
}
