namespace Audit.CRUD.Sample.Infra.Structs
{
    public static class Option
    {
        public static Option<T> Of<T>(T value) => new Option<T>(value, value != null);
    }
}