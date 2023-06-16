namespace SoftwareEngineering2.Profiles;

public static class OrderStatus {
    public const string Received = "received";
    public const string Accepted = "accepted";
    public const string Rejected = "rejected";
    public const string Delivered = "delivered";

    public static bool IsValid(string status) {
        return status is Received or Accepted or Rejected or Delivered;
    }
}