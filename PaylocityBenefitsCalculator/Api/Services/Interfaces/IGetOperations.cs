namespace Api.Services.Interfaces;

public interface IGetOperations<T>
{
    public Task<T> Get(int id);
    public Task<IEnumerable<T>> GetAll();
}

