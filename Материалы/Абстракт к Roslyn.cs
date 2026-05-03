mini-ORM над EF, где схема задаётся через интерфейсы, а всё остальное происходит автоматически
Сделаем архитектуру, которая: выглядит как нативный EF, не ломает tooling, масштабируется, реально годится в NuGet
Условное название - CORM.AutoRelations

1. Состоит из 3 частей:

1.1. Core (контракты / DSL): CORM.AutoRelations

Здесь только интерфейсы, без свойств, без логики:
- public interface IParent<T> {}
- public interface IChildren<T> {}
- public interface IOneToOne<T> {}
- public interface IManyToMany<T> {}

1.2. Generator (Roslyn Source Generator): CORM.AutoRelations.Generator
Генерит:
- FK
- Navigation properties
- optionally Fluent API

1.3. EF Integration (runtime sugar): CORM.AutoRelations.EF
- расширения для DbContext
- доп. автоматизация (опционально)

2. Как это выглядит для пользователя

2.1. Минимальный код

public partial class Order : AEntity, 
	IParent<User>, 
	IParent<Tenant>;
	
2.2. Результат автоматизации

public partial class Order
{
    public int UserId { get; set; }
    public User? User { get; set; }

    public int TenantId { get; set; }
    public Tenant? Tenant { get; set; }
}

3. Ключевые фичи генератора

3.1. Сбор всех IParent<T> и IChildren<T>

var parents = symbol.AllInterfaces
    .Where(i => i.Name == "IParent")
    .Select(i => i.TypeArguments[0]);
// для IChildren аналогично
 
3.2. Генерация свойств

foreach (var parent in parents)
{
    var name = parent.Name;
    sb.AppendLine($"public int {name}Id {{ get; set; }}");
    sb.AppendLine($"public {name}? {name} {{ get; set; }}");
}
// для IChildren аналогично

3. Проверки (!!!)

❌ Ошибка компиляции: class Order : IParent<User>, IParent<User> 
👉 generator: Duplicate relation to User in Order

❌ Нет PK у родителя: 
👉 generator: User must implement IEntity

// Это превращает ORM в compile-time validated DSL

4. Интеграция с EF Integration (чтобы выглядело "нативно")

4.1. extension:

public static class ModelBuilderExtensions
{
    public static void ApplyAutoRelations(this ModelBuilder modelBuilder)
    {
        // опционально:
        // - настройка cascade
        // - naming policy
        // - индексы
    }
}

4.2. В DbContext:

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyAutoRelations();
}

5. Расширения (что сделает пакет реально крутым)

5.1. Настройки через атрибут (опционально)

[RelationConfig(DeleteBehavior = Cascade)]
class Order : IParent<User>

5.2. Naming policy

options.UseCamelCaseFKs();
	
5.3. Генерация обратных коллекций

public ICollection<Order> Orders { get; set; }

(опционально, через флаг)

5.4. Many-to-Many

class User : IManyToMany<Role> //генератор создаёт join entity

5.5. One-to-One

class Profile : IOneToOne<User>

6. NuGet

6.1. Структура

CORM.AutoRelations/
 ├── src/
 │   ├── CORM.AutoRelations/          //(DSL)
 │   ├── CORM.AutoRelations.Generator/
 │   ├── CORM.AutoRelations.EF/
 │
 ├── samples/
 ├── tests/
 └── README.md

6.2. Применение

dotnet add package CORM.AutoRelations // установка

class Order : IParent<User>, IParent<Tenant> // использование


7. Важные нюансы

7.1. partial class ОБЯЗАТЕЛЕН

public partial class Order

7.2. генератор не должен:
- менять существующие свойства
- дублировать
- ломать user code

7.3. нужно проверять:
if (property already exists) → skip

8. Итог

Уровень abstraction как у EF conventions, но с compile-time DSL

✔ краткость
✔ интерфейсы
✔ без ручных свойств
✔ без runtime магии
✔ с compile-time безопасностью
✔ ощущение "это часть EF"

