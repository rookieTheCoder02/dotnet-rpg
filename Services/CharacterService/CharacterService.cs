namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> AddCharater(AddCharacterRequestDto newCharacter)
        {
            var response = new ServiceResponse<List<GetCharacterResponseDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            response.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToListAsync();
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacter(int id)
        {
            var response = new ServiceResponse<List<GetCharacterResponseDto>>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

                if (character is null)
                {
                    throw new Exception($"Character with '{id}' is not found.");
                }

                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();
                response.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToListAsync();

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacters(int userId)
        {

            var response = new ServiceResponse<List<GetCharacterResponseDto>>();

            try
            {
                response.Data = await _context.Characters
                    .Where(c => c.User!.Id == userId)
                    .Select(c => _mapper.Map<GetCharacterResponseDto>(c))
                    .ToListAsync();
                
                if (response is null)
                {
                    throw new Exception("No records in the database.");
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }
            return response;
        }

        public async Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterResponseDto>();
            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
                if(character is null){
                   throw new Exception($"Character with '{id}' is not found."); 
                }
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

        public async Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter)
        {
            var response = new ServiceResponse<GetCharacterResponseDto>();

            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

                if (character is null)
                {
                    throw new Exception($"Character with '{updatedCharacter.Id}' is not found.");
                }

                _mapper.Map(updatedCharacter, character);
                await _context.SaveChangesAsync();

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