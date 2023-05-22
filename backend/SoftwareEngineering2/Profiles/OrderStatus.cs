namespace SoftwareEngineering2;

public static class OrderStatus {
    public const string Received = "received";
    public const string Accepted = "accepted";
    public const string Rejected = "rejected";
    public const string Delivered = "delivered";

    public static bool IsValid(string status) =>
        status == Received || status == Accepted || status == Rejected || status == Delivered;
}
