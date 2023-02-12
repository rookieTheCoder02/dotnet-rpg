namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        static List<Character> characters = new List<Character>{
            new Character(),
            new Character() { Id = 1, Name = "Sam", HitPoints = 150}
        };
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharater(AddCharacterRequestDto newCharacter)
        {

            var response = new ServiceResponse<List<GetCharacterResponseDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            response.Data = characters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList();
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacter(int id)
        {
            var response = new ServiceResponse<List<GetCharacterResponseDto>>();

            try
            {
                var character = characters.FirstOrDefault(c => c.Id == id);

                if (character is null)
                {
                    throw new Exception($"Character with '{id}' is not found");
                }

                characters.Remove(character);

                response.Data = characters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList();

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacters()
        {
            var response = new ServiceResponse<List<GetCharacterResponseDto>>();
            response.Data = characters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterResponseDto>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            response.Data = _mapper.Map<GetCharacterResponseDto>(character);
            return response;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter)
        {
            var response = new ServiceResponse<GetCharacterResponseDto>();

            try
            {
                var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

                if (character is null)
                {
                    throw new Exception($"Character with '{updatedCharacter.Id}' is not found");
                }

                _mapper.Map(updatedCharacter, character);

                // character.Name = updatedCharacter.Name;
                // character.HitPoints = updatedCharacter.HitPoints;
                // character.Strength = updatedCharacter.Strength;
                // character.Defense = updatedCharacter.Defense;
                // character.Intelligence = updatedCharacter.Intelligence;
                // character.Class = updatedCharacter.Class;

                response.Data = _mapper.Map<GetCharacterResponseDto>(character);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }

            return response;
        }
    }
}