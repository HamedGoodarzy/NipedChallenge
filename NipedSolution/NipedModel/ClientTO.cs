namespace NipedModel
{
    public class ClientTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public MedicalDataTO MedicalData { get; set; }
    }
    public class ClientListTO
    {
        public List<ClientTO> Clients { get; set; }
    }
}