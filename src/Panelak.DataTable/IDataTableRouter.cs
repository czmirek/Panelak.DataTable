using System.Threading.Tasks;

namespace Panelak.DataTable
{
    public interface IDataTableRouter
    {
        Task<string> RenderAsync();
    }
}