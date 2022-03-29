namespace WpfList
{
    public interface IUserActionHandling
    {
        string AcceptButtonVisible { get; set; }
        string AddButtonVisible { get; set; }
        string AlertMessage { get; set; }
        string NewUserName { get; set; }
        string NewUserRole { get; set; }
        string NewUserSurname { get; set; }
        string VisibilityState { get; set; }
    }
}