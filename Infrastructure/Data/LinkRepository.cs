using Core.Entities;
using Core.Interfaces;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Helpers;

namespace Infrastructure.Data
{
    public class LinkRepository : ILinkRepository
    {
        private readonly AppIdentityDbContext _context;
        public LinkRepository(AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<UserLink> AddNewLink(UserLink userLink, CancellationToken cancellationToken)
        {
            userLink.ShortenedUrl = LinkHelpers.GenerateShortenedUrl();

            _context.Add(userLink);

            await _context.SaveChangesAsync(cancellationToken);

            return userLink;
        }

        public async Task<UserLink> EditLink(UserLink userLink, CancellationToken cancellationToken)
        {
           var linkFromDb = await GetLinkByShort(userLink.ShortenedUrl, cancellationToken);

            if (linkFromDb == null) return null;

            linkFromDb.OriginalUrl = userLink.OriginalUrl;
            linkFromDb.DisplayName = userLink.DisplayName;

            _context.Entry(linkFromDb).State = EntityState.Modified;

            await _context.SaveChangesAsync(cancellationToken);
            
            return linkFromDb;
        }

        public async Task<UserLink> GetLinkByShort(string linkShort, CancellationToken cancellationToken)
        {
            
            return await _context.UserLinks
                    .Where(x => x.ShortenedUrl == linkShort).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> LinkExistsForUser(string linkShort, string? uid, CancellationToken cancellationToken)
        {
            return await _context.UserLinks.Where(x => x.ShortenedUrl == linkShort 
                && !string.IsNullOrEmpty(x.UserId) && x.UserId == uid).AnyAsync(cancellationToken);
        }

        public async Task<bool> RemoveLink(UserLink userLink, CancellationToken cancellationToken)
        {
            var linkFromDb = await GetLinkByShort(userLink.ShortenedUrl, cancellationToken);

            if (linkFromDb == null) return false;

            _context.Remove(linkFromDb);

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}