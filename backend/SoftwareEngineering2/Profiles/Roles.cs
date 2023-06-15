namespace SoftwareEngineering2;

public static class Roles {
    public const string Client = "client";
    public const string Employee = "employee";
    public const string DeliveryMan = "deliveryman";

    public static bool IsValid(string role) => role == Client || role == Employee || role == DeliveryMan;
}
