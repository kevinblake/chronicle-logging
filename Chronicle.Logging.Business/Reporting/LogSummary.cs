namespace Chronicle.Logging.Business.Reporting
{
    public class LogSummary
    {
        public string Category { get; set; }

        public int Debug { get; set; }

        public int Error { get; set; }

        public int Fatal { get; set; }

        public int Warn { get; set; }

        public int Info { get; set; }
    }
}