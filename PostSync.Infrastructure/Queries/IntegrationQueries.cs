namespace PostSync.Infrastructure.Queries;

public class IntegrationQueries
{
    public static readonly string INSERT_SESSION = "INSERT INTO sessions (email, access_token,refresh_token,platform,user_id,at_expire,rt_expire) " +
                                                   "VALUES (@Email,@AccessToken,@RefreshToken,@Platform,@UserId,@AtExpire,@RtExpire) RETURNING id";

    public static readonly string GET_SESSION = "SELECT * FROM sessions WHERE user_id=@userId";
    public static readonly string GET_SESSION_BY_PLATFORM = "SELECT * FROM sessions WHERE user_id=@UserId and platform=@Platform";
    public static readonly string GET_SESSION_BY_EMAIL = "SELECT * FROM sessions WHERE user_id=@UserId and platform=@Platform and email=@Email";
    public static readonly string UPDATE_INTEGRATION = "UPDATE sessions SET email=@Email,access_token=@AccessToken, refresh_token=@RefreshToken,at_expire=@AtExpire, rt_expire=@RtExpire WHERE id=@Id";
}