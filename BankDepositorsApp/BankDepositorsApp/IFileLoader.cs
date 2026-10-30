using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankDepositorsApp
{
    public interface IFileLoader
    {
        Task<List<T>> LoadAsync<T>(string filePath);
    }
}