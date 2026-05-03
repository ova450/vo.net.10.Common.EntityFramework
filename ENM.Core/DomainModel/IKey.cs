
namespace EntityNexus.DomainModel;

/// <summary>
/// Маркерный интерфейс для сильного типизирования идентификатора.
/// Позволяет в будущем легко переходить на Guid, Ulid, string и другие типы ключей.
/// </summary>
public interface IKey<T>
    where T : IEquatable<T>
{
    T Value { get; }
}

//public readonly record struct ProductId(Guid Value) : IKey<Guid>;
// я не это планировал, но мы еще вернемся к этому, когда будем делать генерацию ключей. Пока что пусть будет просто int.