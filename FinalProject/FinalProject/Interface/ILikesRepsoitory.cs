using FinalProject.DTOs;
using FinalProject.Entities;

namespace FinalProject.Interface
{
    public interface ILikesRepsoitory
    {
        Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);
        Task<AppUser> GetUserWithLikes(int userId);
        Task<IEnumerable<LikeDto>> GetUserLikes(string predicate, int userId);
    }
}
