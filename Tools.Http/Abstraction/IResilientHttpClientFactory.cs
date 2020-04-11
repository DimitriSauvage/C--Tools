using Tools.Http.Resilience;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Http.Abstraction
{
    public interface IResilientHttpClientFactory
    {
        ResilientHttpClient CreateResilientHttpClient();
    }
}
