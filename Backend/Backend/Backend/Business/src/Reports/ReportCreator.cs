namespace Backend.Business.src.Reports
{
    public abstract class ReportCreator
    {
        protected int memberID;
        protected string destination;

        public ReportCreator(int memberID, string destination)
        {
            this.memberID = memberID;
            this.destination = destination;
        }

        public abstract List<string> compileReport(string startDate, string endDate);

    }
}