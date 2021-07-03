namespace Albelli.DataAccessLayer.Model
{
    public interface ICustomeraddress
    {
        string Address { get; set; }
        int Customeraddressid { get; set; }
        int Customerid { get; set; }
    }
}