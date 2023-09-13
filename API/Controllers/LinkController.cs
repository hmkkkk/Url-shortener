using API.Extensions;
using API.Dtos.UserLink;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using API.Dtos;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LinkController : ControllerBase
    {
        private readonly ILinkRepository _linkRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public LinkController(ILinkRepository linkRepo, UserManager<AppUser> userManager, 
            ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _linkRepo = linkRepo;
        }

        [HttpGet("{linkShort}")]
        public async Task<ActionResult<string>> GetRedirectionLinkByShort(string linkShort, CancellationToken cancellationToken) 
        {
            var link = await _linkRepo.GetLinkByShort(linkShort, cancellationToken);

            return link.OriginalUrl;
        }

        [HttpPost]
        public async Task<ActionResult<LinkDto>> CreateNewAnonymousLink(LinkAnonymousFormDto linkDto, CancellationToken cancellationToken)
        {
            var userLink = await _linkRepo.AddNewLink(_mapper.Map<UserLink>(linkDto), cancellationToken); 

            return _mapper.Map<LinkDto>(userLink);
        }

        [Authorize]
        [HttpPost("userLink")]
        public async Task<ActionResult<LinkDto>> CreateNewUserLink(LinkDto linkDto, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindUserByClaimsPrincipal(User);
            
            var linkToDb = _mapper.Map<UserLink>(linkDto);
            linkToDb.UserId = user.Id;

            var userLink = await _linkRepo.AddNewLink(linkToDb, cancellationToken); 

            return _mapper.Map<LinkDto>(userLink);
        }

        [Authorize]
        [HttpDelete("{linkShort}")]
        public async Task<ActionResult> RemoveUserLink(string linkShort, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindUserByClaimsPrincipal(User);

            if (!await _linkRepo.LinkExistsForUser(linkShort, user.Id, cancellationToken)) 
                return NotFound("Link with given short not found.");

            if (await _linkRepo.RemoveLink(await _linkRepo.GetLinkByShort(linkShort, cancellationToken), cancellationToken))
                return NoContent();

            return BadRequest("Failed to remove user link");
        }

        [Authorize]
        [HttpPut("{linkShort}")]
        public async Task<ActionResult<LinkDto>> EditUserLink(LinkDto linkDto, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindUserByClaimsPrincipal(User);

            if (!await _linkRepo.LinkExistsForUser(linkDto.ShortenedUrl, user.Id, cancellationToken)) 
                return NotFound("Link with given short not found.");

            var userLink = await _linkRepo.EditLink(_mapper.Map<UserLink>(linkDto), cancellationToken);

            return _mapper.Map<LinkDto>(userLink);
        }

    }
}