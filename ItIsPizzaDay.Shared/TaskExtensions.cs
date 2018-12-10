namespace ItIsPizzaDay.Shared
{
    using System;
    using System.Threading.Tasks;

    public static class TaskExtensions
    {
        public static async Task IgnoreCancellation(this Task task)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
            }
            catch (AggregateException ex)
            {
                ex.Flatten().Handle(exception => exception is OperationCanceledException);
            }
        }
    }
}