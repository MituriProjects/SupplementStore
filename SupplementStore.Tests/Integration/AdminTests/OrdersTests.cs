﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SupplementStore.Domain.Orders;
using SupplementStore.Domain.Products;
using System.Linq;
using System.Threading.Tasks;

namespace SupplementStore.Tests.Integration.AdminTests {

    [TestClass]
    public class OrdersTests : IntegrationTestsBase {

        [TestMethod]
        public async Task UserIsLoggedOut_RedirectsToLogin() {

            await GetAsync("/Admin/Orders");

            ExamineAuthRedirect("/Admin/Orders");
        }

        [TestMethod]
        public async Task UserIsNotAuthorized_AccessDenied() {

            await GetAsync("/Admin/Orders", TestData.User);

            ExamineAccessDeniedRedirect("/Admin/Orders");
        }

        [TestMethod]
        public async Task UserIsAuthorized_ReturnsOrderDetails() {

            var products = TestEntity.Random<Product>(3);
            var orders = TestEntity.Random<Order>(2);
            orders[0]
                .WithUserId(TestData.Users[0].Id);
            orders[1]
                .WithUserId(TestData.Users[1].Id);
            var orderProducts = TestEntity.Random<OrderProduct>(4);
            orderProducts[0]
                .WithProductId(products[1].ProductId)
                .WithOrderId(orders[1].OrderId);
            orderProducts[1]
                .WithProductId(products[2].ProductId)
                .WithOrderId(orders[0].OrderId);
            orderProducts[2]
                .WithProductId(products[0].ProductId)
                .WithOrderId(orders[0].OrderId);
            orderProducts[3]
                .WithProductId(products[2].ProductId)
                .WithOrderId(orders[1].OrderId);

            await GetAsync("/Admin/Orders", TestData.Admin);

            var contentScheme = ContentScheme.Html();
            foreach (var order in orders) {

                contentScheme.Contains("OrderId", order.OrderId);
                contentScheme.Contains("UserId", order.UserId);
                contentScheme.Contains("UserEmail", TestData.Users.First(e => e.Id == order.UserId).Email);
                contentScheme.Contains("Street", order.Address.Street);
                contentScheme.Contains("PostalCode", order.Address.PostalCode);
                contentScheme.Contains("City", order.Address.City);
                contentScheme.Contains("CreatedOn", order.CreatedOn);

                foreach (var orderProduct in orderProducts.Where(e => e.OrderId == order.OrderId)) {

                    contentScheme.Contains("ProductId", orderProduct.ProductId);
                    contentScheme.Contains("ProductName", products.First(e => e.ProductId == orderProduct.ProductId).Name);
                    contentScheme.Contains("ProductPrice", products.First(e => e.ProductId == orderProduct.ProductId).Price);
                    contentScheme.Contains("Quantity", orderProduct.Quantity);
                }
            }

            Examine(contentScheme);
        }
    }
}