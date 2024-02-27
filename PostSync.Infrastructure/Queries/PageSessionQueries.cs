namespace PostSync.Infrastructure.Queries;

public class PageSessionQueries
{
    public static string INSERT_SESSION = "INSERT INTO page_sessions(page_id,name,access_token,user_id,user_session) VALUES (@PageId,@Name,@AccessToken,@UserId,@UserSession)";
    public static string DELETE_SESSION = "DELETE FROM page_sessions WHERE id=@id";

    public static string UPDATE_SESSION =
        "UPDATE page_sessions SET access_token=@AccessToken WHERE id=@Id";

    public static string GET_SESSION = "SELECT ps.*, s.platform FROM page_sessions ps INNER JOIN  sessions s ON s.id = ps.user_session WHERE ps.user_id=@userId";
    public static string GET_SESSION_BY_PAGE = "SELECT * FROM page_sessions WHERE user_id=@userId AND page_id=@pageId";
    public static string GET_SESSION_BY_PLATFORM =
        "select ps.*, s.platform from page_sessions ps inner join  sessions s on s.id = ps.user_session where s.platform=@platform and ps.user_id=@userId";
}