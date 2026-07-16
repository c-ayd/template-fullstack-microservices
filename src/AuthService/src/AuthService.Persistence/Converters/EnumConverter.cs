using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AuthService.Persistence.Converters
{
    public class EnumConverter<T> : ValueConverter<T, string>
        where T : notnull
    {
        public EnumConverter()
            : base(
            app => app.ToString()!,
            db => (T)Enum.Parse(typeof(T), db))
        {
        }
    }
}