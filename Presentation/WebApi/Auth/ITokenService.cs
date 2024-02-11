namespace WebApi.Auth
{
    public interface ITokenService
    {
        TokenModel GetToken(UserModel user);
    }
}
