using Anamnese.API.Application.Services.Pacient;
using Anamnese.API.Application.Services.Profissional;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.Annotation;
using Anamnese.API.ORM.Repository;

namespace Anamnese.API.Application.Services.Anotation
{
    public class AnotationService : IAnotationService
    {
        private readonly BaseRepository<AnotationModel> _anotationRepository;        
        private readonly IPacientService _pacientService;        
        

        public AnotationService(BaseRepository<AnotationModel> anotationRepository, IPacientService pacientService, IProfissionalService profissionalService)
        {
            _anotationRepository = anotationRepository;
            _pacientService = pacientService;            
        }

        public AnotationModel CreateAnotation(int pacientId, AnnotationCreateModel model)
        {
            
            bool pacientExist = _pacientService.PacientExists(pacientId);
            if(pacientId == null || model == null || pacientExist == false)
            {
                throw new ArgumentNullException();
            }


            var annotation = new AnotationModel{
                Comments = model.Comments,
                CreatedAt = DateTime.Now,
                PacientId = pacientId
            };
            _anotationRepository.Add(annotation);
            _anotationRepository.SaveChanges();
            return annotation;
                                       
        }

        public AnotationModel GetPacientAnotation(int pacientId)
        {
            var anotation = _anotationRepository.GetAll().Where(a => a.PacientId == pacientId).FirstOrDefault();
            return anotation;
        }
    }
}
