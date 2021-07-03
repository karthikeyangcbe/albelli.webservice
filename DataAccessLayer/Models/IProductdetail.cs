namespace Albelli.DataAccessLayer.Model
{
    public interface IProductdetails
    {
        decimal Productdimensioninmm { get; set; }
        short Productid { get; set; }
        string Productname { get; set; }
        short Stackableupto { get; set; }
    }
}