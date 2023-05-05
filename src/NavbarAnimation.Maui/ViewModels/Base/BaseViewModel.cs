using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using NavbarAnimation.Maui.DataStores;
using NavbarAnimation.Maui.Models.Respones.Base;

namespace NavbarAnimation.Maui.ViewModels.Base
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseViewModel : ObservableObject
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    /// <typeparam name="TDataStore"></typeparam>
    public abstract partial class BaseListViewModel<TEntity, TResponse, TDataStore> : BaseViewModel 
        where TEntity : class
        where TResponse : BaseResponse 
        where TDataStore : ReymaBaseDataStore<TEntity, TResponse>
    {
        protected readonly TDataStore DataStore;

        public BaseListViewModel(TDataStore dataStore)
        {
            DataStore = dataStore;
        }

        /// <summary>
        /// 
        /// </summary>
        [ObservableProperty]
        private IEnumerable<TResponse> dataSource;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task InitialLoad()
        {
            var pagedResponse = await DataStore.GetList(pageSize: 20);

            DataSource = pagedResponse.Results;
        }
    }
}
