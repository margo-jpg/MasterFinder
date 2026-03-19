namespace MasterFinder.Domain.Enums
{
    public enum OrdersStatus
    {
        Open = 1,        // открыт для откликов
        InProgress = 2,  // выполняется
        Completed = 3,   // выполнен
        Cancelled = 4    // отменен
    }
}