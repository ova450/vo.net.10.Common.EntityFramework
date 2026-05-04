
namespace EntityNexus.Additionals;

public interface IConcurrency
{
    byte[] RowVersion { get; set; }
}
