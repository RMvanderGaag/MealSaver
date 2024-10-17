using Domain;
using DomainServices.Repositories;
using DomainServices.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MealSaverAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPackageController(IMealPackageRepository mealPackageRepository, IMealPackageService mealPackageService) : ControllerBase
    {
        [HttpPost]
        public async Task<MealPackageResponse> ReserveMealPackage(Guid studentId, Guid mealPackageId)
        {
            var response = new MealPackageResponse();

            switch (await mealPackageService.ReserveMealPackage(mealPackageId, studentId))
            {
                case "success":
                    response.Code = 200;
                    response.Message = "Success";
                    return response;
                case "not-found":
                    response.Code = 404;
                    response.Message = "Meal package or student not found";
                    return response;
                case "already-reserved":
                    response.Code = 400;
                    response.Message = "Meal package already reserved";
                    return response;
                case "not-eighteen":
                    response.Code = 400;
                    response.Message = "Student is not 18+";
                    return response;
                case "already-reservation-on-this-day":
                    response.Code = 400;
                    response.Message = "Student already has a reservation on this day";
                    return response;
                default:
                    response.Code = 500;
                    response.Message = "Internal server error";
                    return response;
            }
        }
    }

    public class MealPackageResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}