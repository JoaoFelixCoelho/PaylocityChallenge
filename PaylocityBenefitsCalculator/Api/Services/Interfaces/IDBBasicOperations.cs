namespace Api.Services.Interfaces;

public interface IDBBasicOperations<TCreate, TUpdate, TDelete, TRead>
{
    Task<TRead> Insert(TCreate elements);
    Task<TRead> Update(TUpdate elements);
    Task Delete(TDelete elements);
}
