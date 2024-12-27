namespace AF.Core.Common;

public class RequestContext
{
    public Guid? UserId
    {
        get
        {
            var id = HttpManager.Current.User.Claims.FirstOrDefault(x => x.Type == "userid")?.Value;
            if (Guid.TryParse(id, out var result))
                return result;
            return null;
        }
    }
}