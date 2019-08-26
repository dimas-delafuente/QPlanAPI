namespace QPlanAPI.Core.Interfaces.UseCases
{
    public interface IOutputPort<in TUseCaseResponse>
    {
        void Handle(TUseCaseResponse response);
    }
}
