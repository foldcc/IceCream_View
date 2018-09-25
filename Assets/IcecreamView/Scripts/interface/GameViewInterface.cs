namespace IcecreamView {
    /// <summary>
    /// icecream页面接口
    /// </summary>
    public interface GameViewInterface
    {
        /// <summary>
        /// 页面被初始化时触发
        /// </summary>
        void OnInitView();
        /// <summary>
        /// 页面被打开时触发
        /// </summary>
        void OnOpenView();
        /// <summary>
        /// 页面被关闭时触发
        /// </summary>
        void OnCloseView();
        /// <summary>
        /// 关闭当前页面
        /// </summary>
        void CloseView();
    }

}

