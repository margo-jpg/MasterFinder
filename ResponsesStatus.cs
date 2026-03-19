namespace MasterFinder.Domain.Enums
{
    public enum ResponsesStatus
    {
        Pending = 1,    // ожидает решения заказчика
        Accepted = 2,   // принят заказчиком
        Rejected = 3,   // отклонен заказчиком
        Withdrawn = 4   // отозван исполнителем
    }
}