namespace Panelak.DataTable
{
    public record PostContext
    {
        public string ControllerUrl { get; init; }
        public string ReturnUrl { get; init; }
        public string UserId { get; init; }
        public string TableIdentifier { get; init; }
    }
}
