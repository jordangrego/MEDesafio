using ME.Desafio.Model.Status;

namespace ME.Desafio.Service.Status
{
    public interface IStatusService
    {
        StatusResponseModel ChangeStatus(StatusRequestModel model);
    }
}