namespace TDS.Infrastructure.PauseScreen
{
    public interface IPauseScreenService : IService
    {
        void ShowScreen();
        void HideScreen();
    }
}