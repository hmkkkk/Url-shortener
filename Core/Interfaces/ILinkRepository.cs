using Core.Entities;

namespace Core.Interfaces
{
    public interface ILinkRepository
    {
        Task<UserLink> GetLinkByShort(string linkShort, CancellationToken cancellationToken);
        Task<UserLink> AddNewLink(UserLink userLink, CancellationToken cancellationToken);
        Task<bool> RemoveLink(UserLink userLink, CancellationToken cancellationToken);
        Task<UserLink> EditLink(UserLink userLink, CancellationToken cancellationToken);
        Task<bool> LinkExistsForUser(string linkShort, string? uid, CancellationToken cancellationToken);
    }
}