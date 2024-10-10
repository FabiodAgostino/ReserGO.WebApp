using ReserGO.DTO;
using ReserGO.DTO.ListAvailability;
using ReserGO.Utils.DTO.Utils;

namespace ReserGO.Miscellaneous.Extensions
{
    public static class DTOUserExtension
    {
        public static DTOUserLight GetFromDTOSession(this DTOUserSession user)
        {
            var u = new DTOUserLight()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Username
            };
            return u;
        }
    }
}
