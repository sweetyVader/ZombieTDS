namespace TDS.Infrastructure.LoadingScreen
{
    public interface ILoadingScreenService : IService
    {
        void ShowScreen();
        void HideScreen();
        
    }
}