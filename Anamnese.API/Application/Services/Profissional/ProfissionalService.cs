using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.ProfissionalModel;
using Anamnese.API.ORM.Repository;

namespace Anamnese.API.Application.Services.Profissional
{
    public class ProfissionalService : IProfissionalService
    {
        private readonly BaseRepository<ProfissionalModel> _profisionalRepository;
        
        public ProfissionalModel CreateProfissional(CreateProfissionalModel createUserModel)
        {
            //Verificar se email já existe
            if (_profisionalRepository.FindAll(e => e.Email == createUserModel.Email).Any()) 
            {
                throw new Exception("E-mail já está em uso.");
            }
            var salt = BCrypt.Net.BCrypt.GenerateSalt(10);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(createUserModel.Password, salt);


            ProfissionalModel newUser = new ProfissionalModel
            {
                Username = createUserModel.Username,
                Email = createUserModel.Email,
                Password = hashedPassword,
            };

            var createdUser = _profisionalRepository.Add(newUser);
            _profisionalRepository.SaveChanges();
            return createdUser;
        }

        public List<ProfissionalModel> GetDoctorsWithPatients()
        {
            //Ainda a definir
            return new List<ProfissionalModel> { };

        }

        public async Task<ProfissionalModel> GetUserByEmailAsync(string email)
        {
            return await _profisionalRepository.FindAsync(u => u.Email == email);
        }

        public bool IsEmailTaken(string email)
        {
            return _profisionalRepository.FindAll(e => e.Email == email).Any();
        }

        public async Task<bool> ValidateCredentials(string email, string password)
        {
            var user = await _profisionalRepository.FindAsync(u => u.Email == email);
            if (user == null)
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
    }
}
