using ResultManager.Model;

namespace ResultManager.Managers
{
    public interface IPlayerManager
    {
        PlayerDetail GetPlayerDetail(string fullName);
    }
}
