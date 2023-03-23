namespace Backend.Business.src.Reports
{
    public class SMSReportCreator : ReportCreator
    {
        public SMSReportCreator(int memberID, string destination) : base(memberID, destination)
        {
        }

        public override List<string> compileReport(string startDate, string endDate)
        {
            throw new NotImplementedException();
        }
    }
}