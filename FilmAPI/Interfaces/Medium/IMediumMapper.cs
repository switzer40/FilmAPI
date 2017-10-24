using FilmAPI.Common.DTOs.Medium;

namespace FilmAPI.Interfaces.Medium
{
    public interface IMediumMapper : IHomebrewMapper<Core.Entities.Medium, BaseMediumDto>
    {
        Core.Entities.Medium MapBackForce(BaseMediumDto b);        
    }
}
