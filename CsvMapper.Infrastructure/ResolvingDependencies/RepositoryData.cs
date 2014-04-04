using CsvMapper.Core.Interfaces;
using CsvMapper.Core.Services;
using CsvMapper.Infrastructure.Data;

namespace CsvMapper.Infrastructure.ResolvingDependencies
{
    public class RepositoryData
    {
        public IDataRepository DataRepository { get; set; }
        public RepositoryData()
        {
            DataRepository = new ETLService(new CsvDataRepository()).DataRepository;
        }
    }
}
