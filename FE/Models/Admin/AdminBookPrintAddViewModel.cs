namespace FE.Models.Admin
{
    public class AdminBookPrintAddViewModel
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public IEnumerable<string> Branches { get; set; }
        public string SelectedBranch { get; set; }
    }
}
