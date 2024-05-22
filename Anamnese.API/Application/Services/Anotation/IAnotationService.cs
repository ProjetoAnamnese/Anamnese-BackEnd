using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.Annotation;

namespace Anamnese.API.Application.Services.Anotation
{
    public interface IAnotationService
    {
       AnotationModel GetPacientAnotation(int pacientId);
        AnotationModel CreateAnotation(int pacientId,AnnotationCreateModel model);
    }
}
