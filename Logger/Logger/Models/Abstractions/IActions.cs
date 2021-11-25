namespace Logger.Models.Abstractions
{
    public interface IActions
    {
        void BrokenMethod();
        void SucceedMethod();
        void WarnedMethod();
    }
}