namespace WpfList.Core
{
    public class AplicationUser : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }
        public bool IsSelected { get; set; }
    }
}
