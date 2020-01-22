namespace DAL.Domain
{
    public enum DeliveryType
    {
        New,
        Processing,
        SuccessInTime,
        SuccessOutOfTime,
        Failed,
        Aborted,
    }
}
