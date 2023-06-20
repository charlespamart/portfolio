using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.Azure.WebJobs;

namespace Presentation.Functions;

public sealed class TimeTriggerServiceBusQueueBinding
{
    [FunctionName("TimeTriggerServiceBusBinding")]
    public async Task RunAsync([TimerTrigger("*/15 * * * * *")] TimerInfo myTimer,
        [ServiceBus("%ServiceBus:QueueName%", Connection = "ServiceBus:ConnectionString")]
        IAsyncCollector<Todo> collector, CancellationToken cancellationToken)
    {
        await collector.AddAsync(new Todo
        {
            Id = Guid.NewGuid(),
            Name = "This Todo was sent in a service bus",
        }, cancellationToken);
    }
}