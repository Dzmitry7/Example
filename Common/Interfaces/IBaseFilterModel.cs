namespace Models.Interfaces
{
    public interface IBaseFilterModel 
    {
        int Take { get; set; }

        int Skip { get; set; }

        string Token { get; set; } 
    }
}
