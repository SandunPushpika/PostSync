namespace PostSync.Infrastructure.Queries;

public class UserQueries
{
    public static readonly string GET_BY_USERNAME = "SELECT * FROM public.user WHERE username=@Username";
    public static readonly string INSERT_USER = "INSERT INTO public.user(username, first_name, last_name, password, status) VALUES (@Username, @FirstName, @LastName, @Password, @Status)";
}