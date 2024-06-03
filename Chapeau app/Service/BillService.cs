using System;
using System.Collections.Generic;
using Model;
using DAL;

namespace Service
{
    public class BillService
    {
        private readonly BillDao _billDao;

        public BillService()
        {
            _billDao = new BillDao();
        }

        public void AddBill(Bill bill)
        {
            try
            {
                _billDao.AddBill(bill);
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while adding the bill.", ex);
            }
        }

        public Bill GetBill(int id)
        {
            try
            {
                return _billDao.GetBill(id);
            }
            catch (Exception ex)
            {
                
                throw new Exception($"An error occurred while retrieving the bill with ID {id}.", ex);
            }
        }

        public List<Bill> GetBills()
        {
            try
            {
                return _billDao.GetBills();
            }
            catch (Exception ex)
            {
                // Handle exception, e.g., log the error
                throw new Exception("An error occurred while retrieving the list of bills.", ex);
            }
        }

        public void UpdateFeedback(int billId, string feedback)
        {
            try
            {
                _billDao.UpdateFeedback(billId, feedback);
            }
            catch (Exception ex)
            {
                
                throw new Exception($"An error occurred while updating the feedback for the bill with ID {billId}.", ex);
            }
        }

        public void UpdateTip(int billId, float tip)
        {
            try
            {
                _billDao.UpdateTip(billId, tip);
            }
            catch (Exception ex)
            {
              
                throw new Exception($"An error occurred while updating the tip for the bill with ID {billId}.", ex);
            }
        }

        public void DeleteBill(Bill bill)
        {
            try
            {
                _billDao.DeleteBill(bill);
            }
            catch (Exception ex)
            {
                
                throw new Exception($"An error occurred while deleting the bill with ID {bill.Id}.", ex);
            }
        }
    }
}
