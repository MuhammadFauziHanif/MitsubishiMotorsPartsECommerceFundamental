using MitsubishiMotorsPartsECommerce.BLL.Interfaces;
using MitsubishiMotorsPartsECommerce.DAL.MyWebFormApp.DAL.Interfaces;
using MitsubishiMotorsPartsECommerce.DAL;
using System;
using System.Collections.Generic;
using System.Text;
using MitsubishiMotorsPartsECommerce.Interface;
using MitsubishiMotorsPartsECommerce.BLL.DTOs;

namespace MitsubishiMotorsPartsECommerce.BLL
{
    public class SalesOrderBLL : ISalesOrderBLL
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

        public IEnumerable<SalesOrderHeaderDTO> GetSalesOrderHeaderByCustomerID(int customerID)
        {
            if (customerID <= 0)
            {
                throw new ArgumentException("CustomerID is required");
            }
            try
            {
                List<SalesOrderHeaderDTO> salesOrderHeaderDTOs = new List<SalesOrderHeaderDTO>();
                var salesOrderHeaders = _salesOrderDAL.GetSalesOrderHeaderByCustomerID(customerID);
                foreach (var salesOrderHeader in salesOrderHeaders)
                {
                    salesOrderHeaderDTOs.Add(new SalesOrderHeaderDTO
                    {
                        OrderID = salesOrderHeader.OrderID,
                        CustomerID = salesOrderHeader.CustomerID,
                        OrderDate = salesOrderHeader.OrderDate,
                        TotalAmount = salesOrderHeader.TotalAmount,
                        OrderStatus = salesOrderHeader.OrderStatus
                    });
                }
                return salesOrderHeaderDTOs;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
