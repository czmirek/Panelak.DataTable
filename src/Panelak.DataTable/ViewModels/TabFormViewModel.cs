namespace Panelak.DataTable
{
    internal record TabFormViewModel : BaseViewModel
    {
        public string Operation { get; init; }
        public string ReturnUrl { get; init; }
        public string UserId { get; init; }
    }
}