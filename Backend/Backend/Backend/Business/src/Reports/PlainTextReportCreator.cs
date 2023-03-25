namespace Backend.Business.src.Reports
{
    public class PlainTextReportCreator : ReportCreator
    {
        public PlainTextReportCreator(int memberID, string destination) : base(memberID, destination)
        {
        }

        public override List<string> compileReport(string startDate, string endDate)
        {
            throw new NotImplementedException();
        }
    }
}