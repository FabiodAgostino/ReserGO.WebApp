using ReserGO.DTO;
using ReserGO.DTO.ListAvailability;
using ReserGO.Utils.DTO.Utils;

namespace ReserGO.Miscellaneous.Extensions
{
    public static class DTOUserExtension
    {
        public static DTOUser GetFromDTOSession(this DTOUserSession user)
        {
            var u = new DTOUser()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Username
            };
            return u;
        }
    }
}
