namespace SoftwareEngineering2.Profiles;

public static class Roles {
    public const string Client = "client";
    public const string Employee = "employee";
    public const string DeliveryMan = "deliveryman";

    public static bool IsValid(string role) => role is Client or Employee or DeliveryMan;
}
