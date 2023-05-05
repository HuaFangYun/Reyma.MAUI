namespace NavbarAnimation.Maui.Models.Respones.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseResponse<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        public TKey Id { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseResponse : BaseResponse<Guid>
    { 
    
    }
}
