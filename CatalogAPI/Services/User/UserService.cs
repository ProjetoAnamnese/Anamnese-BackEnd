using CatalogAPI.Context;
using CatalogAPI.Models;
using CatalogAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Services.User
{
    public class UserService : IUserService
    {
        private readonly BaseRepository<DoctorModel> _userRepository;

        public UserService(BaseRepository<DoctorModel> userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsEmailTaken(string email)
        {
            return _userRepository.FindAll(e => e.Email == email).Any();
        }

        public DoctorModel CreateUser(CreateUserModel userModel)
        {
            //Verificar se email já existe
            if (_userRepository.FindAll(e => e.Email == userModel.Email).Any())
            {
                throw new Exception("E-mail já está em uso.");
            }

            //Criptografar senha
            var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userModel.Password, salt);

            DoctorModel newUser = new DoctorModel
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                Password = hashedPassword
            };

            var createdUser = _userRepository.Add(newUser);
            _userRepository.SaveChanges();
            return createdUser;
        }


        public async Task<bool> ValidateCredentials(string email, string password)
        {
            var user = await _userRepository.FindAsync(u => u.Email == email);

            if (user != null)
            {
                bool isPasswordCorrect = VerifyPassword(password, user.Password);

                return isPasswordCorrect;
            }

            return false;
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedPassword);
        }

        public async Task<DoctorModel> GetUserByEmailAsync(string email)
        {
            return await _userRepository.FindAsync(u => u.Email == email);
        }

        public List<DoctorModel> GetDoctorsWithPatients()
        {
            //return _context.Doctor.Include(u => u.Patients).Where(u => u.Patients.Any()).ToList();
            //return _userRepository.FindAll(u => u.Patients.Any());
            return new List<DoctorModel> { };
        }
    }
}
