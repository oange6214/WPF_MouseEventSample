using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace WPFCanvas_Move_Drog.ViewModels
{

    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<PointItemViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public PointItemViewModel PointItem => ServiceLocator.Current.GetInstance<PointItemViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
