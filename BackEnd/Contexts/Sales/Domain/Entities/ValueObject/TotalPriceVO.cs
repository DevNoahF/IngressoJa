namespace IngressoJa.Contexts.Sales.Domain.Entities.ValueObject
{
    public class TotalPriceVO
    {
        public double Value { get; private set; }

        public TotalPriceVO(double value)
        {
            if (value < 0)
                throw new Exception("The total value cannot be negative.");

            Value = value;
        }
    }
}
