namespace Infrastructure.Memory.Generators;

public interface IGenericGenerator<K>
{
    K Next { get; }
    K Current { get; }
}