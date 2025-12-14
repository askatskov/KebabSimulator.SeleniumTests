public static class TestUserFactory
{
    public static string NewEmail()
        => $"test_{Guid.NewGuid():N}@example.com";

    public const string ValidPassword = "Test123!";
}
