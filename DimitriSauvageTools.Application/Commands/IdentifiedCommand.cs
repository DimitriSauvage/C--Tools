using System;
using MediatR;

namespace DimitriSauvageTools.Application.Commands
{
    public class IdentifiedCommand<TCommand, TResponse> : IRequest<TResponse>
        where TCommand : IRequest<TResponse>
    {
        public TCommand Command { get; }
        public Guid Id { get; }
        public IdentifiedCommand(TCommand command, Guid id)
        {
            Command = command;
            Id = id;
        }
    }
}
