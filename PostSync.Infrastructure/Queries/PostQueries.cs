namespace PostSync.Infrastructure.Queries;

public class PostQueries
{
    public static string INSERT_POST = "INSERT INTO post (title,time,time_zone,description,images,integration_id, user_id, status) VALUES (@Title, @Time, @TimeZone, @Description, @Images::varchar[], @IntegrationId, @UserId, @Status)";
    public static string GET_ALL_POST = "SELECT * FROM post WHERE user_id=@UserId";
    public static string DELETE_POST = "DELETE FROM post WHERE id=@Id";
    public static string UPDATE_POST = "UPDATE post SET title=@Title,time=@Time,time_zone=@TimeZone,description=@Description,images=@Images::varchar[],integration_id=@IntegrationId, user_id=@UserId WHERE id=@Id";
}