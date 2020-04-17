using Tools.Http.Resilience;

namespace Tools.Http.Abstraction
{
    public interface IResilientHttpClientFactory
    {
        ResilientHttpClient CreateResilientHttpClient();
    }
}
