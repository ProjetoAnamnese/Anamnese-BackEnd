using CatalogAPI.Models;

namespace CatalogAPI.Services.User
{
    public interface IUserService
    {
        bool IsEmailTaken(string email);     
        List<DoctorModel> GetDoctorsWithPatients();
        DoctorModel CreateUser(CreateUserModel createUserModel);        
        Task<bool> ValidateCredentials(string email, string password);
        Task<DoctorModel> GetUserByEmailAsync(string email);


    }
}
