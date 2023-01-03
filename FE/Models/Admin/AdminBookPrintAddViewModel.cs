namespace FE.Models.Admin
{
    public class AdminBookPrintAddViewModel
    {
        public IEnumerable<string> Books { get; set; }
        public IEnumerable<string> Branches { get; set; }
        public string SelectedBook { get; set; }
        public string SelectedBranch { get; set; }
    }
}
