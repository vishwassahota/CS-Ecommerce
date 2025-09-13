using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_Project_1030.Utility
{
    public static class SD
    {
        //CoverType Stored Procedure
        public const string proc_GetCoverTypes = "GetCoverTypes";
        public const string proc_updateCoverType = "UpdateCoverType";
        public const string proc_GetCoverType = "GetCoverType";
        public const string proc_DeleteCoverType = "DeleteCoverType";
        public const string proc_CreateCoverType = "CreateCoverType";

        //Roles
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee User";
        public const string Role_Company = "Company User";
        public const string Role_Individual = "Individual User";

        //Session
        public const string Ss_CartSessionCount = "Cart Count Session";

        //OrderStatus
        public const string OrderStatusPending = "Pending";
        public const string OrderStatusApproved = "Approved";
        public const string OrderStatusInProgress = "Processing";
        public const string OrderStatusShipped = "Shipped";
        public const string OrderStatusCancelled = "Cancelled";
        public const string OrderStatusRefunded = "Refunded";

        //PaymentStatus
        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusDelayPayment = "PaymentStatusDelay";
        public const string PaymentStatusRejected = "Rejected";

        public static double GetPriceBasedOnQuantity(double quantity,double price,double price50,double price100)
        {
            if (quantity < 50)
                return price;
            else if (quantity < 100)
                return price50;
            else
                return price100;
        }
        public static string ConvertToRawHtml(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;
            for (int i=0; i < source.Length; i++)
            {
                char let = source[i]; //</p>
                if(let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if(!inside)
                {
                    array[arrayIndex] = let; //Hello Hi Welcome
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }


    }
}
