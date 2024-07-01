using System;
using System.Collections.Generic;
using Model;
using DAL;

namespace Service
{
    public class SubBillService
    {
        private readonly SubBillDao _subBillDao;

        public SubBillService()
        {
            _subBillDao = new SubBillDao();
        }

        public List<SubBill> GetAllSubBills()
        {
            try
            {
                return _subBillDao.GetAllSubBills();
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while retrieving all sub-bills.", ex);
            }
        }

        public void AddSubBill(SubBill subBill)
        {
            try
            {
                _subBillDao.AddSubBill(subBill);
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while adding the sub-bill.", ex);
            }
        }

        public void UpdateSubBill(SubBill subBill)
        {
            try
            {
                _subBillDao.UpdateSubBill(subBill);
            }
            catch (Exception ex)
            {
               
                throw new Exception($"An error occurred while updating the sub-bill with ID {subBill.Id}.", ex);
            }
        }

        public void DeleteSubBill(int subBillId)
        {
            try
            {
                _subBillDao.DeleteSubBill(subBillId);
            }
            catch (Exception ex)
            {
              
                throw new Exception($"An error occurred while deleting the sub-bill with ID {subBillId}.", ex);
            }
        }

        public List<SubBill> GetSubBillByBillId(int billId)
        {
            try
            {
                return _subBillDao.GetSubBillByBillId(billId);
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Asn error occurred while retrieving sub-bills for the bill with ID {billId}.", ex);
            }
        }
        public int GetLastSubBillId()
        {
            try
            {
                return _subBillDao.GetLastSubBillId();
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while retrieving the last sub-bill ID.", ex);
            }
        }
    }
}
