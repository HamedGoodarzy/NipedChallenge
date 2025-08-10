namespace NipedModel;
public class ClientReportTO
{
    public ClientTO Client { get; set; } = new ClientTO();
    public List<ClientReportEntryTO> ReportEntries { get; set; } = new List<ClientReportEntryTO>();
}

