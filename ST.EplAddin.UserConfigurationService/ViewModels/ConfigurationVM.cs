using ST.EplAddin.UserConfigurationService.Models;

namespace ST.EplAddin.UserConfigurationService.ViewModels
{
    internal class ConfigurationVM : ViewModel
    {
        public UserConfigurationShemes _userConfigurationShemes { get; set; }


        public ConfigurationVM(UserConfigurationShemes userConfigurationShemes)
        {
            _userConfigurationShemes = userConfigurationShemes;
        }

        public ConfigurationVM()
        {

        }

    }
}
