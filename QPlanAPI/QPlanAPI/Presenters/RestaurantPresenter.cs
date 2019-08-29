using System.Net;
using Microsoft.AspNetCore.Mvc;

using QPlanAPI.Core.DTO;
using QPlanAPI.Core.Interfaces.UseCases;
using QPlanAPI.Serialization;

namespace QPlanAPI.Presenters
{
    public class RestaurantPresenter : IOutputPort<UseCaseResponse>
    {
        public ContentResult Result { get; set; }

        public RestaurantPresenter()
        {
            Result = new ContentResult();
            Result.ContentType = "application/json";
        }


        public void Handle(UseCaseResponse response)
        {
            Result.StatusCode = (int) (response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            Result.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
