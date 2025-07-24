using System;
using MediatR;
using Todo.Domain.Entities;

namespace Todo.Domain.Events;

public record TodoItemCreatedEvent(TodoItem Item) : INotification;