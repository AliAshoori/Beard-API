namespace WildBeard.Orders.Model
{
    public enum CancellationReason
    {
        Expensive = 0,
        TakingTooMuchTimeToBeDelivered,
        DoNotNeedThemAnyMore,
        OrderedByMistake,
        Other
    }
}
