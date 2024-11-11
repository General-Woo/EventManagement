using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using System.Linq.Expressions;

namespace EventManagement.Server.UseCases.Users
{
    public static class UserMapping
    {
        public static Expression<Func<User, UserDto>> UserProjection
        {
            get
            {
                return d => new UserDto
                {
                    Id = d.Id,
                    FullName = d.FullName,
                    Email = d.Email,
                    PhoneNumber = d.PhoneNumber
                };
            }
        }
    }
}

