namespace Task1QQQ.ViewModels
{
    public interface ICloseWindow
    {
        Action Close { get; set; }
        bool CanClose();
    }
}
