using System;
using Microsoft.Extensions.Options;

namespace DimitriSauvageTools.Infrastructure.Abstraction
{
    public interface IWritableOptions<out T> : IOptionsSnapshot<T> where T : class, new()
    {
        void Update(Action<T> applyChanges);
    }

}
