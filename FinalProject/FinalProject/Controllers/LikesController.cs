using FinalProject.DTOs;
using FinalProject.Entities;
using FinalProject.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Authorize]
    public class LikesController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public LikesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpPost("{username}")]
        public async Task<IActionResult> AddLike(string username)
        {
            var sourceUserId = User.GetUserId();
            var likedUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
            var sourceUser = await _unitOfWork.LikesRepsoitory.GetUserWithLikes(sourceUserId);

            if(likedUser == null) return NotFound();
            if (sourceUser.UserName == username) return BadRequest("You can't like yourself");
            var userLike = await _unitOfWork.LikesRepsoitory.GetUserLike(sourceUserId, likedUser.Id);
            if (userLike != null) return BadRequest("Already Liked") ;

            userLike = new UserLike
            {
                sourceUserId = sourceUserId,
                LikedUserId = likedUser.Id,
            };
            sourceUser.LikedUsers.Add(userLike);
            if (await _unitOfWork.Complete()) return Ok();
            return BadRequest("Failed to like user");

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes(string predicate)
        {
            var user = await _unitOfWork.LikesRepsoitory.GetUserLikes(predicate, User.GetUserId());
            return Ok(users);
        }
    }
}
