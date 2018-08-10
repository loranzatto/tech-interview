using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using tech_interview.backend.Model;

namespace tech_interview.backend
{
    class Program
    {       
        public static void Main()
        {
            IDataRepository _dataRepository = new DataRepository();
            Root root = null;
            try
            {
                Console.WriteLine("Execution start...");
                for (int i = 1; i <= 3; i++)
                {
                    switch (i)
                    {
                        //LEVEL 1 
                        case 1:
                            Console.WriteLine("Reading data from level 1 logic and executing logic...");
                            root = _dataRepository.getDataFromLevels(i.ToString());
                            runLevel1Logic(ref root);
                            break;
                        //LEVEL 2
                        case 2:
                            Console.WriteLine("Reading data from level 2 logic and executing logic...");
                            root = _dataRepository.getDataFromLevels(i.ToString());
                            runLevel2Logic(ref root);
                            break;
                        //LEVEL 3
                        case 3:
                            Console.WriteLine("Reading data from level 3 logic and executing logic...");
                            root = _dataRepository.getDataFromLevels(i.ToString());
                            runLevel3Logic(ref root);
                            break;
                    }
                    //Including a reader (Carts) into the object result
                    var wrapper = new
                    {
                        carts = root.Carts
                    };
                    //Serialize JSON to a string ignoring null values
                    string jsonIgnoreNullValues = JsonConvert.SerializeObject(wrapper, Formatting.Indented, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                    //Then write string to a file
                    Console.WriteLine("Writing output data from level "+ i +" logic into folder...");
                    Console.WriteLine("");

                    string path = ConfigurationManager.AppSettings["level" + i + "WriteData"];
                    File.WriteAllText(path, jsonIgnoreNullValues);
                }
                Console.WriteLine("Execution done! Press any key to exit!");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }
        /// <summary>
        /// Method to execute the need logic to implement Level 1 task from tech interview project
        /// </summary>
        /// <param name="root">Object with whole data recovered from JSON File (Articles and Carts)</param>
        private static void runLevel1Logic(ref Root root)
        {
            foreach (Carts cart in root.Carts)
            {
                int totalPrice = 0;
                
                foreach (Items item in cart.Items)
                {
                    //Level 1 Logic
                    int articlePrice = root.Articles.Where(s => s.Id == item.ArticleId).FirstOrDefault().Price;
                    totalPrice += articlePrice * item.Quantity;
                }
                cart.Total = totalPrice;
                //Needed to generate Carts result object in a format to be possible put it into a JSON File (Id and Total)
                cart.Items = null;
            }
        }
        /// <summary>
        /// Method to execute the need logic to implement Level 2 task from tech interview project
        /// </summary>
        /// <param name="root">Object with whole data recovered from JSON File (Articles, Carts and Deliveries Fees)</param>
        private static void runLevel2Logic(ref Root root)
        {
            foreach (Carts cart in root.Carts)
            {
                int totalCustomerFees = 0;
                // If there is no Item to get the Delivery free when Item = 0
                if (cart.Items.Count == 0)
                {
                    totalCustomerFees += root.DeliveryFees.Where(s => s.EligibleTransaction.MinPrice <= 0
                                                             && (s.EligibleTransaction.MaxPrice == null
                                                              || s.EligibleTransaction.MaxPrice >= 0)).SingleOrDefault().Price;
                }
                foreach (Items item in cart.Items)
                {
                    //Level 2 Logic
                    int articlePrice = root.Articles.Where(s => s.Id == item.ArticleId).FirstOrDefault().Price;
                    int totalByItem = articlePrice * item.Quantity;
                    int customerFees = root.DeliveryFees.Where(s => s.EligibleTransaction.MinPrice <= totalByItem
                                                                && (s.EligibleTransaction.MaxPrice == null
                                                                 || s.EligibleTransaction.MaxPrice >= totalByItem)).SingleOrDefault().Price;
                    totalCustomerFees += customerFees;


                }
                cart.Total = totalCustomerFees;
                //Needed to generate Carts result object in a format to be possible put it into a JSON File (Id and Total)
                cart.Items = null;
            }
        }
        /// <summary>
        /// Method to execute the need logic to implement Level 2 task from tech interview project
        /// </summary>
        /// <param name="root">Object with whole data recovered from JSON File (Articles, Carts, Deliveries Fees and Discounts)</param>
        private static void runLevel3Logic(ref Root root)
        {
            foreach (Carts cart in root.Carts)
            {
                int totalPrice = 0;

                foreach (Items item in cart.Items)
                {
                    //Level 3 Logic
                    int articlePrice = root.Articles.Where(s => s.Id == item.ArticleId).FirstOrDefault().Price;
                    double itemPrice = articlePrice * item.Quantity;
                   
                    var discountRslt = root.Discounts.Where(s => s.ArticleId == item.ArticleId).SingleOrDefault();
                    if(discountRslt != null)
                    {
                        Discounts discount = (Discounts)discountRslt;
                        if (discount.Type == "amount")
                        {
                            itemPrice = itemPrice - (discount.Value * item.Quantity);
                        }
                        else
                        {
                            itemPrice = itemPrice * ((double) (100 - discount.Value)  / 100);
                        }
                    }

                    totalPrice += (int)itemPrice;
                }
                int customerFees = root.DeliveryFees.Where(s => s.EligibleTransaction.MinPrice <= totalPrice
                                                            && (s.EligibleTransaction.MaxPrice == null
                                                             || s.EligibleTransaction.MaxPrice >= totalPrice)).SingleOrDefault().Price;
                totalPrice = totalPrice + customerFees;
                cart.Total = totalPrice;
                //Needed to generate Carts result object in a format to be possible put it into a JSON File (Id and Total)
                cart.Items = null;
            }
        }
    }
}
