using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUseCase<TRequest, TResponse>
    {
        Task<TResponse> Execute(TRequest request);
    }

    public interface IUseCase<TResponse>
    {
        Task<TResponse> Execute();
    }
}
