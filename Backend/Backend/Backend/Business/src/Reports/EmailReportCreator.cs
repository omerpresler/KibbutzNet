namespace Backend.Business.src.Reports
{
    public class EmailReportCreator : ReportCreator
    {
        public EmailReportCreator(int memberID, string destination) : base(memberID, destination)
        {
        }

        public override List<string> compileReport(string startDate, string endDate)
        {
            throw new NotImplementedException();
        }
    }
}