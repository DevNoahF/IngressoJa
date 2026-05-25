namespace IngressoJa.Contexts.Sales.Domain.Entities.ValueObject
{
    public class SelectedTicketsUserVO
    {
        public int Value { get; private set; }

        public SelectedTicketsUserVO(int value)
        {
            if (value <= 0)
                throw new Exception("The quantity must be greater than zero.");

            Value = value;
        }
    }
}
