using BookStore.Models.Models.Users;

namespace BookStode.DL.Interfaces
{
    public interface IUserInfoRepository
    {
        public Task<UserInfo?> GetUserInfoAsync(string email, string password);
    }
}
