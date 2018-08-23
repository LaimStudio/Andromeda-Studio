using AndromedaStudio.Classes;
using Dragablz;

namespace AndromedaStudio.ViewModels
{
    class MainWindowViewModel
    {
        public IInterTabClient InterTabClient { get; set; }
        public MainWindowViewModel()
        {
            InterTabClient = new InterTabClient();
        }
    }
}
