namespace CsvReader
{
    using System.Collections.Generic;

    public interface ICsvReader
    {
        List<T> Parse<T>(string path) where T : new();
    }
}
