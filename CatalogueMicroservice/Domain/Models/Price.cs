namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

record Price
{
    public Price(float Value)
    {
        if(!Validate(Value))
            return;
        
        this.Value = Value;
    }

    public bool Validate(float value) => value < 0.0;

    public Price Add(Price toAdd) => new Price(Value + toAdd.Value);

    public Price Reduce(Price toReduce) => new Price(Value - toReduce.Value);

    public float Value { get; }
}