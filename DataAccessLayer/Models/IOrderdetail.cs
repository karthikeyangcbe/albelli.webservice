namespace Albelli.DataAccessLayer.Model
{
    public interface IOrderdetail
    {
        int Orderdetailsid { get; set; }
        int Orderid { get; set; }
        int Producttypeid { get; set; }
    }
}