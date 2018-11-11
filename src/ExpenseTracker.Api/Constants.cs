namespace ExpenseTracker.Api
{
    internal static class Constants
    {
        public static class Policy
        {
            public const string Management = nameof(Management);
            public const string EndUser = nameof(EndUser);
            public const string Any = nameof(Any);
        }

        public static class Scopes
        {
            public const string Manage = "e_t.manage";
            public const string TrackExpenses = "e_t.user.trackexpenses";
        }
    }
}
