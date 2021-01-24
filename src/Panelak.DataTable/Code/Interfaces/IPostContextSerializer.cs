namespace Panelak.DataTable
{
    internal interface IPostContextSerializer
    {
        PostContext Deserialize(string data);
        string Serialize(PostContext context);
    }
}