using PetShop.Domain.DTO;
using PetShop.Domain.Entities;
using PetShop.Domain.Identity;
using PetShop.Repository.Interface;
using PetShop.Service.Interface;
using PetShop.Service.Mappers;

public class AdoptionApplicationService : IAdoptionApplicationService
{
    protected readonly IRepository<Pet> petRepository;
    protected readonly IRepository<AdoptionApplication> _repository;
    protected readonly IUserRepository userRepository;

    public AdoptionApplicationService(IRepository<Pet> petRepository,
        IUserRepository userRepository,
        IRepository<AdoptionApplication> repository)
    {
        this.userRepository = userRepository;
        this.petRepository = petRepository;
        _repository = repository;
    }

    public AdoptionApplicationDTO DeleteById(string id)
    {
        var application = _repository.Delete(Guid.Parse(id));
     
        return application.toDTO();
    }

    public List<AdoptionApplicationDTO> FindAll()
    {
        return _repository.GetAll().Select(a => a.toDTO()).ToList();
    }

    public AdoptionApplicationDTO FindById(string id)
    {
        var application = _repository.Get(Guid.Parse(id)) ?? throw new Exception("Adoption application not found.");

        return application.toDTO();
    }

    public AdoptionApplicationDTO Store(AdoptionApplicationDTO appDTO)
    {
        var pet = petRepository.Get(appDTO.PetId) ?? throw new Exception("Pet not found.");
		
        var user = userRepository.Get(appDTO.ApplicantId) ?? throw new Exception("Applicant not found.");
		var application = appDTO.toAdopApp(user, pet);
        _repository.Insert(application);
        return application.toDTO();
    }

    public AdoptionApplicationDTO Update(string id, AdoptionApplicationDTO appDTO)
    {
		var application = _repository.Get(Guid.Parse(id)) ?? throw new Exception("Adoption application not found.");
        var pet = petRepository.Get(appDTO.PetId) ?? throw new Exception("Pet not found.");
		var user = userRepository.Get(appDTO.ApplicantId) ?? throw new Exception("Applicant not found.");

		application.updateAdopApp(appDTO, user, pet);
        _repository.Update(application);
        return application.toDTO();
    }
}
