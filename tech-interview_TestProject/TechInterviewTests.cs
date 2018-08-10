using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using tech_interview.backend.Model;

namespace tech_interview_TestProject
{
    [TestClass]
    public class TechInterviewTests
    {
        public Root initializeTestParameters()
        {
            //Initializing parameters
            Articles article = new Articles();
            article.Id = 1;
            article.Name = "water";
            article.Price = 100;

            Items item = new Items();
            item.ArticleId = 1;
            item.Quantity = 6;

            Carts cart = new Carts();
            cart.Id = 1;
            cart.Items.Add(item);

            EligibleTransactionVolume eligibleTransaction = new EligibleTransactionVolume();
            eligibleTransaction.MinPrice = 0;
            eligibleTransaction.MaxPrice = 999;

            DeliveryFees delivery = new DeliveryFees();
            delivery.EligibleTransaction = eligibleTransaction;
            delivery.Price = 800;

            Discounts discount = new Discounts();
            discount.ArticleId = 1;
            discount.Type = "amount";
            discount.Value = 25;

            Root root = new Root();
            root.Articles.Add(article);
            root.Carts.Add(cart);
            root.DeliveryFees.Add(delivery);
            root.Discounts.Add(discount);

            return root;
        }
      
        //Level 1 Test
        [TestMethod]
        public void SumArticlePriceShouldBe600()
        {
            Root root = initializeTestParameters();           
            
            //Logic Testing
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

            Assert.AreEqual(600, root.Carts[0].Total);   
        }

        //Level 2 Test
        [TestMethod]
        public void DeliveryFeeShouldBe800()
        {
            Root root = initializeTestParameters();

            //Logic Testing
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

            Assert.AreEqual(800, root.Carts[0].Total);
        }
        //Level 3 Test
        [TestMethod]
        public void PriceWithDiscountShouldBe1250()
        {
            Root root = initializeTestParameters();

            //Logic Testing
            foreach (Carts cart in root.Carts)
            {
                int totalPrice = 0;

                foreach (Items item in cart.Items)
                {
                    //Level 3 Logic
                    int articlePrice = root.Articles.Where(s => s.Id == item.ArticleId).FirstOrDefault().Price;
                    double itemPrice = articlePrice * item.Quantity;

                    var discountRslt = root.Discounts.Where(s => s.ArticleId == item.ArticleId).SingleOrDefault();
                    if (discountRslt != null)
                    {
                        Discounts discount = (Discounts)discountRslt;
                        if (discount.Type == "amount")
                        {
                            itemPrice = itemPrice - (discount.Value * item.Quantity);
                        }
                        else
                        {
                            itemPrice = itemPrice * ((double)(100 - discount.Value) / 100);
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

            Assert.AreEqual(1250, root.Carts[0].Total);
        }
    }
}
