using DbHandler;

namespace WpfList.Core
{
    public static class AplicationUserMapper
    {
        public static AplicationUser MappUser(AplicationUserModel user) => new AplicationUser
        {
            Id = user.Id,
            IsSelected = user.IsSelected,
            Name = user.Name,
            Role = user.Role,
            Surname = user.Surname
        };
    }
}
