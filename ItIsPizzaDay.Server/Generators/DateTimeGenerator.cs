namespace ItIsPizzaDay.Server.Generators
{
    using System;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.ValueGeneration;

    public class DateTimeGenerator : ValueGenerator<DateTime>
    {
        public override DateTime Next(EntityEntry entry) => DateTime.UtcNow;

        public override bool GeneratesTemporaryValues { get; } = false;
    }
}