using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankDepositorsApp
{
    public interface IFileSaver
    {
        Task SaveAsync<T>(string filePath, List<T> data);
    }
}