namespace Contracts;

public static class APIEndpoints
{
    public const string ApiBase = "api";

    public static class Customer
    {
        private const string Base = $"{ApiBase}/customer";

        public const string Create = Base;
        public const string Get = $"{Base}/{{id}}";
        public const string GetAll = Base;
        public const string Update = $"{Base}/{{id}}";
        public const string Delete = $"{Base}/{{id}}";
    }
}
