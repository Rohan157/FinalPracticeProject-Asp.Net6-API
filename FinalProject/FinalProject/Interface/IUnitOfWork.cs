namespace FinalProject.Interface
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ILikesRepsoitory LikesRepsoitory { get; }
        Task<bool> Complete();
        bool HasChanges();

    }
}
