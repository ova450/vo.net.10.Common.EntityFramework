namespace EntityNexus.Additionals;

/// <summary>
/// Включает полный аудит (Created + Modified)
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class AuditableAttribute : Attribute { }

/// <summary>
/// Включает историю изменений (отдельная таблица)
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class TrackHistoryAttribute : Attribute { }

/// <summary>
/// Для случаев множественных родителей
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class HasParentAttribute(Type parentType) : Attribute
{
    public Type ParentType { get; } = parentType;
}
