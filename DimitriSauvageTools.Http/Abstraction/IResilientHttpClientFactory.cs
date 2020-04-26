using DimitriSauvageTools.Http.Resilience;

namespace DimitriSauvageTools.Http.Abstraction
{
    public interface IResilientHttpClientFactory
    {
        ResilientHttpClient CreateResilientHttpClient();
    }
}
