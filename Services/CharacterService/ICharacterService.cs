
namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
         Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacters();
         Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(int id);
         Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharater(AddCharacterRequestDto newCharacter);
         Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter);
         Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacter(int id);
         Task<ServiceResponse<GetCharacterResponseDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);
    }
}