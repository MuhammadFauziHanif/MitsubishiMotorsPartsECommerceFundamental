using MitsubishiMotorsPartsECommerce.BLL.Interfaces;
using MitsubishiMotorsPartsECommerce.DAL.MyWebFormApp.DAL.Interfaces;
using MitsubishiMotorsPartsECommerce.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using MitsubishiMotorsPartsECommerce.Interface;

namespace MitsubishiMotorsPartsECommerce.BLL
{
    public class SalesOrderBLL : ISalesOrder
    {
        private readonly ISalesOrderDAL _salesOrderDAL;

        public SalesOrderBLL()
        {
            _salesOrderDAL = new SalesOrderDAL();
        }

        public int Create(DTOs.SalesOrderCreateDTO salesOrderCreate)
        {
            if (salesOrderCreate.CustomerID <= 0)
            {
                throw new ArgumentException("CustomerID is required");
            }
            if (string.IsNullOrEmpty(salesOrderCreate.LstProd))
            {
                throw new ArgumentException("String order is required");
            }

            try
            {
                return _salesOrderDAL.Create(salesOrderCreate.CustomerID, salesOrderCreate.LstProd);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
